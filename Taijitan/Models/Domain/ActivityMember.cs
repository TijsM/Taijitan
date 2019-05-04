using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan.Models.Domain
{
    public class ActivityMember
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int MemberId { get; set; }
        public  Member Member { get; set; }


        public ActivityMember(int activityId, Activity activity, int memberId,Member member )
        {
            ActivityId = activityId;
            Activity = activity;
            MemberId = memberId;
            Member = member;
        }

        public ActivityMember()
        {

        }
    }
}
