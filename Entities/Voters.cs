﻿using System;
using System.Security.Permissions;

namespace eVotingApp.Entities
{
    public class Voters
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string HouseAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
