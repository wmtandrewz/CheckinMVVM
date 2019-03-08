using System;
using System.Collections.Generic;
using CheckinMVVM.Models;

namespace CheckinMVVM.Globals
{
    public static class Constants
    {
        public static string AccessToken { get; set; }
        public static string TokenExpiresIn { get; set; }


        public static ReservationsHeaderModel SelectedReservationHeader { get; set; }
        public static ReservationDetailsModel SelectedReservationDetailSet { get; set; }
        public static GuestDetailsModel SelectedGuestProfile { get; set; }
        public static GuestSignatureModel SelectedGuestSignature { get; set; }
        public static NoticesRemarksModel SelectedNoticesRemarksSet { get; set; }
        public static RemarksModel SelectedRemark { get; set; }
        public static List<GuestSignatureModel> SignaturesList = new List<GuestSignatureModel>();

        public static List<IDMethodModel> IdentificationMethods = new List<IDMethodModel> { 

            new IDMethodModel
            {
                Method = "National ID",
                Code = "1"
            },
            new IDMethodModel
            {
                Method = "Passport",
                Code = "2"
            }
        };

        public static List<GenderModel> Genders = new List<GenderModel> {

            new GenderModel
            {
                Gender = "Male",
                Code = "1"
            },
            new GenderModel
            {
                Gender = "Female",
                Code = "2"
            }
        };

        public static List<SalutationModel> Salutations = new List<SalutationModel> {

            new SalutationModel
            {
                Salutation = "Ms.",
                Code = "0001"
            },
            new SalutationModel
            {
                Salutation = "Mr.",
                Code = "0002"
            },

            new SalutationModel
            {
                Salutation = "Mrs.",
                Code = "0011"
            },
            new SalutationModel
            {
                Salutation = "Company",
                Code = "0003"
            },
            new SalutationModel
            {
                Salutation = "Mr. and Mrs.",
                Code = "0004"
            },

            new SalutationModel
            {
                Salutation = "Professor",
                Code = "0006"
            },

            new SalutationModel
            {
                Salutation = "Hon.",
                Code = "0007"
            },

            new SalutationModel
            {
                Salutation = "Rev",
                Code = "0008"
            },

            new SalutationModel
            {
                Salutation = "Dr.",
                Code = "0009"
            },

            new SalutationModel
            {
                Salutation = "HE",
                Code = "0010"
            },

            new SalutationModel
            {
                Salutation = "INF",
                Code = "0012"
            },

            new SalutationModel
            {
                Salutation = "CHD",
                Code = "0013"
            },

            new SalutationModel
            {
                Salutation = "Ven",
                Code = "0014"
            }
        };

    }
}
