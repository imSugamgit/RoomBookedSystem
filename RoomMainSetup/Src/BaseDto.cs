using System;
using System.Collections.Generic;
using System.Text;

namespace RoomMainSetup.Src
{
   public  class BaseDto
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? BranchId { get; set; }
    }
}
