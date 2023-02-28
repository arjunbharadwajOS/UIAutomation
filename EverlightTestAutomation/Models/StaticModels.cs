namespace EverlightTestAutomation.Models
{
    public static class StaticModels
    {
        public static Hashtable Modality()
        {
            return new Hashtable(){
                {"MR", "MRI (MR)"},
                {"CT", "CT (CT)"},
                {"XR", "Xray (XR)"},
                {"US","Ultrasound (US)" }
            };
        }

        public static Hashtable OrgCodeList()
        {
            return new Hashtable(){
                {"LUM", "Lumus (LUM)"},
                {"CUK", "Care UK (CUK)"},
                {"USC", "The Ultrasound Clinic (USC)"}
            };
        }

        public static Hashtable SiteIdList_Org1()
        {
            return new Hashtable(){
                {"101", "Northern Beaches Hospital"},
                {"102", "Baulkham Hills"},
                {"103", "Ingleburn"},
                {"104","Camden Nuclear Medicine" },
                {"105","St George Private Hospital" }
            };
        }

        public static Hashtable SiteIdList_Org2()
        {
            return new Hashtable(){
                {"201", "Sussex"},
                {"202", "Lincoln"},
                {"203", "Spalding"}
            };
        }

        public static Hashtable SiteIdList_Org3()
        {
            return new Hashtable(){
                {"301", "Clinic"}
            };
        }

        public static Hashtable Status()
        {
            return new Hashtable(){
                {"SC", "the order is scheduled"},
                {"IP", "images are in progress of being sent"},
                {"CM", "images are complete"},
                {"NG","the study is not for reporting" },
                {"CMD","the report is in transcription" },
                {"ZZ","the report is ready" },
                {"CA","the order has been cancedlled" }
            };
        }
            
    }
}
