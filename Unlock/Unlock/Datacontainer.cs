﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unlock
{
    class Datacontainer
    {
        public static string connectionString;
        public static string anvandarnamn;
        public static string losen;
        public static string connectsource;
        public static string personnummer;
        public static string Familyname;
        public static string fornamn;
        public static SqlConnection cnn;
        public static SqlCommand command, command2;
        public static int Index;
        public static int[] Indexarray = new int[1000000];
        public static string SQLSearch;
        public static SqlDataReader reader,reader2;
    }
}
