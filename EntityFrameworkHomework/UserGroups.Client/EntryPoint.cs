namespace UserGroups.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UserGroups.Models;

    public class EntryPoint
    {
        public static void Main()
        {
            string userName = "Pesho";
            string groupName = "Admin";

            using (var userDB = new UserGroupsEntities())
            {
                using (var transaction = userDB.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new User();
                        user.Name = userName;

                        var group = userDB.Groups.Where(x => x.Name == groupName).FirstOrDefault();
                        if (group == null)
                        {
                            group = new Group() { Name = groupName };
                        }

                        userDB.Groups.Add(group);
                        userDB.Users.Add(user);
                        userDB.SaveChanges();

                        var userGroup = new UserGroup()
                        {
                            UserId = userDB.Users.FirstOrDefault(x => x.Name == userName).ID,
                            GroupId = userDB.Groups.FirstOrDefault(x => x.Name == groupName).ID
                        };

                        userDB.UserGroups.Add(userGroup);

                        userDB.SaveChanges();
                        transaction.Commit();
                        Console.WriteLine("Successfully Inserted");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
