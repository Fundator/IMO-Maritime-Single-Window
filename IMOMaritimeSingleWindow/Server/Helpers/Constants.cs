
using System;
using System.Collections.Generic;

namespace IMOMaritimeSingleWindow.Helpers
{
    public static class Constants
    {
        public static class Integers
        {
            public static class DatabaseTableIds
            {
                public const int ORGANIZATION_TYPE_GOVERNMENT_AGENCY = 1;
                public const int ORGANIZATION_TYPE_COMPANY = 2;
                public const int OTHER_PURPOSE_ID = 100249;
                public const int PORT_CALL_STATUS_ACTIVE = 100232;
                public const int PORT_CALL_STATUS_CANCELLED = 100233;
                public const int PORT_CALL_STATUS_COMPLETED = 100234;
                public const int PORT_CALL_STATUS_DRAFT = 100235;
            }
        }

        public static class Strings
        {
            public static class DatabaseTableStrings
            {
                public const string CLAIM_TYPE_PORT_CALL_NAME = "Port Call";
                public const string CLAIM_TYPE_MENU_NAME = "Menu";
            }
            public static class Claims
            {
                public static class Types
                {
                    public const string PORT_CALL = "Port Call",
                                        MENU = "Menu",
                                        USER = "User",
                                        SHIP = "Ship",
                                        LOCATION = "Location",
                                        ORGANIZATION = "Organization";
                }
                public static class Values
                {
                    public const string REGISTER = "Register",   // C - Create
                                        VIEW = "View",       // R - Read
                                        EDIT = "Edit",       // U - Update
                                        DELETE = "Delete",     // D - Delete
                                        CLEARANCE = "Clearance";
                    public static class MenuEntries
                    {
                        public const string USERS = "USERS";
                        public const string SHIPS = "SHIPS";
                        public const string LOCATION = "LOCATIONS";
                        public const string COMPANIES = "COMPANIES";
                        public const string PORT_CALL = "PORT CALL";
                    }
                }
            }

            // Taken from https://github.com/mmacneil/AngularASPNETCore2WebApiAuth/blob/master/src/Helpers/Constants.cs
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class PersonClaims
            {
                public const string Register = "register", View = "view", Delete = "delete";
            }

            public static class UserRoles
            {
                public const string Admin = "admin", Agent = "agent", Customs = "customs", HealthAgency = "health_agency", SuperAdmin = "super_admin";
            }

            public static class Policies
            {
                public const string AdminRole = "AdminRole", SuperAdminRole = "SuperAdminRole";
            }




        }
        public enum LoginStates
        {
            OK,
            InvalidCredentials,
            LockedOut,
            Requires2FA //To be implemented?
        }
    }
}
