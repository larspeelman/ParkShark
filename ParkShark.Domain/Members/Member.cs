﻿using System;
using System.Collections.Generic;
using System.Text;


namespace ParkShark.Domain.Members
{
    public class Member
    {
        public string MemberId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        private Member()
        {
        }

        private Member(string firstName, string lastName, Address address)
        {
            MemberId = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            RegistrationDate = DateTime.Now;
        }

        public static Member CreateMember(string firstName, string lastName, Address address)
        {
            if (string.IsNullOrWhiteSpace(firstName)|| string.IsNullOrWhiteSpace(lastName) || address == null)
            {
                return null;
            }
            return new Member(firstName, lastName, address);
        }
    }
}
