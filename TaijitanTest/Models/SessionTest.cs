using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taijitan.Models.Domain;
using TaijitanTest.Data;
using Xunit;

namespace TaijitanTest.Models
{
    public class SessionTest
    {
        private readonly DummyApplicationDbContext _dummyApplicationContext;
        public SessionTest()
        {
            _dummyApplicationContext = new DummyApplicationDbContext();
        }

        #region Constructor
        [Fact]
        public void NewSession_ValidData_CreatesSession()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members)
            {
                SessionId = 1,
            };
            Assert.Equal(1, s.SessionId);
            Assert.Equal(_dummyApplicationContext.Members, s.Members);
            Assert.False(s.SessionStarted);
            Assert.Empty(s.MembersPresent);
            Assert.Equal(DateTime.Now, s.Date, new TimeSpan(0, 0, 1)); //assert wordt heeeeeel kleine tijd later uitgevoerd. normale equal zal dus false teruggeven
            Assert.Equal(_dummyApplicationContext.Formulas, s.Formulas);
            Assert.Equal(_dummyApplicationContext.Teacher1, s.Teacher);
            Assert.NotNull(s.TrainingDay); //hangt af van welke dag het nu is
            Assert.Empty(s.SessionMembers);
            Assert.Empty(s.NonMembers);
            Assert.Equal(5, s.SessionFormulas.Count);
        }
        #endregion

        #region Methods
        [Fact]
        public void AddToMembersPresent_validMember_AddsMembertoPresent()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToMembersPresent(s.Members.First());
            Assert.Single(s.MembersPresent);
        }
        [Fact]
        public void AddToMembersPresent_NonvalidMember_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToMembersPresent(null);
            Assert.Empty(s.MembersPresent);
        }
        [Fact]
        public void AddToMembers_validMember_AddsMembertoPresent()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToMembersPresent(s.Members.First());
            s.AddToMembers(s.MembersPresent.First());
            Assert.Empty(s.MembersPresent);
        }
        [Fact]
        public void AddToMembers_NonvalidMember_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToMembers(null);
            Assert.Equal(_dummyApplicationContext.Members,s.Members);
        }
        [Fact]
        public void AddToSessionMembers_ValidList_AddsMembersToSessionMembers()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToMembersPresent(_dummyApplicationContext.Members.First());
            s.AddToMembersPresent(_dummyApplicationContext.Members.Skip(1).First());
            s.AddToSessionMembers(s.MembersPresent.ToList());
            Assert.Equal(2, s.SessionMembers.Count());
        }
        [Fact]
        public void AddToSessionMembers_EmptyList_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToSessionMembers(s.MembersPresent.ToList());
            Assert.Empty(s.SessionMembers);
        }
        [Fact]
        public void AddToSessionMembers_Null_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddToSessionMembers(null);
            Assert.Empty(s.SessionMembers);
        }
        [Fact]
        public void AddToNonMember_ValidMember_AddsNonMemberToList()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddNonMember(new NonMember("Jarne", "Deschacht", "jarne.deschacht@gmail.com", s.SessionId));
            Assert.Single(s.NonMembers);
        }
        [Fact]
        public void AddToNonMember_NonValidMember_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.AddNonMember(new NonMember("Jarne", "Deschacht", "jarne.deschacht@gmail.com", s.SessionId));
            Assert.Single(s.NonMembers);
        }
        [Fact]
        public void RemoveNonMember_ValidMember_RemovesNonMember()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            NonMember non = new NonMember("Jarne", "Deschacht", "jarne.deschacht@gmail.com", s.SessionId);
            s.AddNonMember(non);
            s.RemoveNonMember(non);
            Assert.Empty(s.NonMembers);
        }
        [Fact]
        public void RemoveNonMember_NonValidMember_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            NonMember non = new NonMember("Jarne", "Deschacht", "jarne.deschacht@gmail.com", s.SessionId);
            s.AddNonMember(non);
            s.RemoveNonMember(null);
            Assert.Single(s.NonMembers);
        }
        [Fact]
        public void StartSession_SessionWasNotYetStarted_StartsSession()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members);
            s.Start();
            Assert.True(s.SessionStarted);
        }
        [Fact]
        public void StartSession_SessionWasAlreadyStarted_DoesNothing()
        {
            Session s = new Session(_dummyApplicationContext.Formulas, _dummyApplicationContext.Teacher1, _dummyApplicationContext.Members) { SessionStarted = true };
            s.Start();
            Assert.True(s.SessionStarted);
        }
        #endregion
    }
}
