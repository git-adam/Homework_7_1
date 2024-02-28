using Homework_7_1.Models.Domains;
using Homework_7_1.Models.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Homework_7_1.Models.Converters;
using Homework_7_1.Models;
using System.Windows;

namespace Homework_7_1
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context
                    .Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)                  
                    .AsQueryable();

                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);

                return students
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingsDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(context, student);

                var studentsRatings = GetStudentsRatings(context, student);

                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Math);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Technology);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.Physics);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.ForeignLang);
                UpdateRate(student, ratings, context, studentsRatings,
                    Subject.PolishLang);

                context.SaveChanges();
            }
        }

        private static List<Rating> GetStudentsRatings(ApplicationDbContext context,
            Student student)
        {
            return context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();
        }

        private void UpdateStudentProperties(ApplicationDbContext context,
            Student student)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.Activities = student.Activities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student, List<Rating> newRatings, ApplicationDbContext context, List<Rating> studentsRatings, Subject subject)
        {
            var subRatings = studentsRatings
                    .Where(x => x.SubjectId == (int)subject)
                    .Select(x => x.Rate);

            var newSubRatings = newRatings
                .Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var subRatingsToDelete = GetSubRatingsToDelete(subRatings, newSubRatings);
            var subRatingsToAdd = GetSubRatingsToAdd(subRatings, newSubRatings);

            //var subRatingsToDelete = subRatings.Except(newSubRatings).ToList();
            //var subRatingsToAdd = newSubRatings.Except(subRatings).ToList();

            //subRatingsToDelete.ForEach(x =>
            //{
            //    var ratingToDelete = context.Ratings.First(y =>
            //        y.Rate == x &&
            //        y.StudentId == student.Id &&
            //        y.SubjectId == (int)subject);

            //    context.Ratings.Remove(ratingToDelete);
            //});

            //subRatingsToAdd.ForEach(x =>
            //{
            //    var ratingToAdd = new Rating
            //    {
            //        Rate = x,
            //        StudentId = student.Id,
            //        SubjectId = (int)subject
            //    };
            //    context.Ratings.Add(ratingToAdd);
            //});
        }
        private static List<int> GetSubRatingsToAdd(
            IEnumerable<int> oldSubRatings, IEnumerable<int> newSubRatings)
        {
            var subRatingsToAdd = new List<int>();
            var oldListCopy = new List<int>(oldSubRatings);
            foreach (var item in newSubRatings)
            {
                var itemInOldList = oldListCopy.FirstOrDefault(x => x == item);
                if (itemInOldList != 0)
                    oldListCopy.Remove(itemInOldList);
                else
                    subRatingsToAdd.Add(item);
            }
            return subRatingsToAdd;
        }

        private static List<int> GetSubRatingsToDelete(
            IEnumerable<int> oldSubRatings, IEnumerable<int> newSubRatings)
        {
            var subRatingsToDelete = new List<int>();
            var newListCopy = new List<int>(newSubRatings);
            foreach (var item in oldSubRatings)
            {
                var itemInNewList = newListCopy.FirstOrDefault(x => x == item);
                if (itemInNewList != 0)
                    newListCopy.Remove(itemInNewList);
                else
                    subRatingsToDelete.Add(item);
            }
            return subRatingsToDelete;
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingsDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });

                context.SaveChanges();
            }
        }
    }
}
