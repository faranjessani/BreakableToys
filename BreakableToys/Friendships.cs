using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class Friendships
    {
        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public User(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        class Friendship
        {
            public int UserId1 { get; set; }
            public int UserId2 { get; set; }

            public Friendship(int userId1, int userId2)
            {
                UserId1 = userId1;
                UserId2 = userId2;
            }
        }

        [Test]
        public void FriendshipsTest1()
        {
            var users = new User[2]
            {
                new User(1, "Larry"),
                new User(2, "Anthony")
            };
            var friendships = new Friendship[1] {new Friendship(1, 2)};

            CalculateImpact(users, friendships, 1).Should().Be(2);
        }

        [Test]
        public void FriendshipTest2()
        {
            var users = new[]
            {
                new User(1, "Larry"),
                new User(2, "Anthony"),
                new User(3, "Bob"),
                new User(5, "Jimmy"),
            };
            var friendships = new[]
            {
                new Friendship(1, 2),
//                new Friendship(2,3), 
                new Friendship(1, 3),
                new Friendship(3, 5),
            };
            CalculateImpact(users, friendships, 1).Should().Be(4);
        }

        private int CalculateImpact(User[] users, Friendship[] friendships, int userId)
        {
            var friendshipGraph = new Dictionary<int, HashSet<int>>();
            foreach (var friendship in friendships)
            {
                HashSet<int> user1List, user2List;
                if (!friendshipGraph.TryGetValue(friendship.UserId1, out user1List))
                {
                    user1List = new HashSet<int>();
                    friendshipGraph[friendship.UserId1] = user1List;
                }

                if (!friendshipGraph.TryGetValue(friendship.UserId2, out user2List))
                {
                    user2List = new HashSet<int>();
                    friendshipGraph[friendship.UserId2] = user2List;
                }

                user1List.Add(friendship.UserId2);
                user2List.Add(friendship.UserId1);
            }

            var visited = new HashSet<int>();
            CalculateImpactHelper(friendshipGraph, userId, visited);

            return visited.Count;
        }

        private void CalculateImpactHelper(Dictionary<int, HashSet<int>> friendshipGraph, int userId,
            HashSet<int> visited)    
        {
            visited.Add(userId);
            var friends = friendshipGraph[userId];
            foreach (var friend in friends)
            {
                if (!visited.Contains(friend))
                    CalculateImpactHelper(friendshipGraph, friend, visited);
            }
        }
    }
}