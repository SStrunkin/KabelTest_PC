﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelTest
{
    class Example_topology_Class1
    {
        private String s000 = ("*000000000000000000 10000000000000000000*00000000000000000 010000000000000000000*0000000000000000 "
            + "0000000000000000000000*000000000000000 00010000000000000000000*00000000000000 000010000000000000000000*0000000000000 "
            + "0000010000000000000000000*000000000000 00000010000000000000000000*00000000000 000000010000000000000000000*0000000000 "
            + "0000000010000000000000000000*000000000 00000000010000000000000000000*00000000 000000000010000000000000000000*0000000 "
            + "0000000000000000000000000000000*000000 00000000000010000000000000000000*00000 000000000000010000000000000000000*0000 "
            + "0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000000000010000000000000000000*0 "
            + "0000000000000000010000000000000000000* 00000000000000000001000000000000000000 *0000000000000000000100000000000000000 "
            + "0*000000000000000000000000000000000000 00*00000000000000000001000000000000000 000*0000000000000000000100000000000000 "
            + "0000*000000000000000000010000000000000 00000*00000000000000000001000000000000 000000*0000000000000000000100000000000 "
            + "0000000*000000000000000000010000000000 00000000*00000000000000000001000000000 000000000*0000000000000000000100000000 "
            + "0000000000*000000000000000000000000000 00000000000*00000000000000000001000000 000000000000*0000000000000000000100000 "
            + "0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000000000000100 "
            + "0000000000000000*000000000000000000010 00000000000000000*00000000000000000000 000000000000000000*");

        private String s004_1 = ("*000000000000000000 00010000000000000000*00000000000000000 000010000000000000000*0000000000000000 0000010000000000000000*000000000000000 00000010000000000000000*00000000000000 000000010000000000000000*0000000000000 0000000010000000000000000*000000000000 00000000010000000000000000*00000000000 000000000010000000000000000*0000000000 0000000000001000000000000000*000000000 00000000000000001000000000000*00000000 000000000000000000000000000000*0000000 0000000000000000000000000000000*000000 00000000000000000000000000000000*00000 000000000000000000000000000000000*0000 0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000000000000000000000000000000*0 0000000000000000000000000000000000000* 00000000000000000000000000000000000000 *0000000000000000000000000000000000000 0*000000000000000000000000000000000000 00*00000000000000001000000000000000000 000*0000000000000000100000000000000000 0000*000000000000000010000000000000000 00000*00000000000000001000000000000000 000000*0000000000000000100000000000000 0000000*000000000000000010000000000000 00000000*00000000000000001000000000000 000000000*0000000000000000100000000000 0000000000*000000000000000000000000000 00000000000*00000000000000010000000000 000000000000*0000000000000000000000000 0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000001000000000 0000000000000000*000000000000000000000 00000000000000000*00000000000000000000 000000000000000000*");

        private String s004_2 = ("*000000000000000000 00000000000000000000*00000000000000000 000000000000000000000*0000000000000000 0000000000000000000000*000000000000000 10000000000000000000000*00000000000000 010000000000000000000000*0000000000000 0010000000000000000000000*000000000000 00010000000000000000000000*00000000000 000010000000000000000000000*0000000000 0000010000000000000000000000*000000000 00000010000000000000000000000*00000000 000000010000000000000000000000*0000000 0000000000000000000000000000000*000000 00000000100000000000000000000000*00000 000000000000000000000000000000000*0000 0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000100000000000000000000000000*0 0000000000000000000000000000000000000* 00000000000000000000001000000000000000 *0000000000000000000000100000000000000 0*000000000000000000000010000000000000 00*00000000000000000000001000000000000 000*0000000000000000000000100000000000 0000*000000000000000000000010000000000 00000*00000000000000000000001000000000 000000*0000000000000000000000100000000 0000000*000000000000000000000001000000 00000000*00000000000000000000000000100 000000000*0000000000000000000000000000 0000000000*000000000000000000000000000 00000000000*00000000000000000000000000 000000000000*0000000000000000000000000 0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000000000000000 0000000000000000*000000000000000000000 00000000000000000*00000000000000000000 000000000000000000*");

        private String s005_1 = ("*000000000000000000 00010000000000000000*00000000000000000 000010000000000000000*0000000000000000 0000010000000000000000*000000000000000 00000010000000000000000*00000000000000 000000000000000000000000*0000000000000 0000000000000000000000000*000000000000 00000000010000000000000000*00000000000 000000000010000000000000000*0000000000 0000000000001000000000000000*000000000 00000000000000001000000000000*00000000 000000000000000000000000000000*0000000 0000000000000000000000000000000*000000 00000000000000000000000000000000*00000 000000000000000000000000000000000*0000 0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000000000000000000000000000000*0 0000000000000000000000000000000000000* 00000000000000000000000000000000000000 *0000000000000000000000000000000000000 0*000000000000000000000000000000000000 00*00000000000000001000000000000000000 000*0000000000000000100000000000000000 0000*000000000000000010000000000000000 00000*00000000000000001000000000000000 000000*0000000000000000000000000000000 0000000*000000000000000000000000000000 00000000*00000000000000001000000000000 000000000*0000000000000000100000000000 0000000000*000000000000000000000000000 00000000000*00000000000000010000000000 000000000000*0000000000000000000000000 0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000001000000000 0000000000000000*000000000000000000000 00000000000000000*00000000000000000000 000000000000000000*");

        private String s005_2 = ("*000000000000000000 00000000000000000000*00000000000000000 000000000000000000000*0000000000000000 0000000000000000000000*000000000000000 10000000000000000000000*00000000000000 010000000000000000000000*0000000000000 0010000000000000000000000*000000000000 00010000000000000000000000*00000000000 000000000000000000000000000*0000000000 0000000000000000000000000000*000000000 00000010000000000000000000000*00000000 000000010000000000000000000000*0000000 0000000000000000000000000000000*000000 00000000100000000000000000000000*00000 000000000000000000000000000000000*0000 0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000100000000000000000000000000*0 0000000000000000000000000000000000000* 00000000000000000000001000000000000000 *0000000000000000000000100000000000000 0*000000000000000000000010000000000000 00*00000000000000000000001000000000000 000*0000000000000000000000000000000000 0000*000000000000000000000000000000000 00000*00000000000000000000001000000000 000000*0000000000000000000000100000000 0000000*000000000000000000000001000000 00000000*00000000000000000000000000100 000000000*0000000000000000000000000000 0000000000*000000000000000000000000000 00000000000*00000000000000000000000000 000000000000*0000000000000000000000000 0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000000000000000 0000000000000000*000000000000000000000 00000000000000000*00000000000000000000 000000000000000000*");

        private String s036 = ("*000000000000000000 00000000000000000000*00000000000000000 010000000000000000000*0000000000000000 0010000000000000000000*000000000000000 00000000000000000000000*00000000000000 000010000000000000000000*0000000000000 0000010000000000000000000*000000000000 00000000000000000000000000*00000000000 000000010000000000000000000*0000000000 0000000000000000000000000000*000000000 00000000010000000000000000000*00000000 000000000000000000000000000000*0000000 0000000000000000000000000000000*000000 00000000000000000000000000000000*00000 000000000000000000000000000000000*0000 0000000000000000000000000000000000*000 00000000000000000000000000000000000*00 000000000000000000000000000000000000*0 0000000000000000000000000000000000000* 00000000000000000000000000000000000000 *0000000000000000000100000000000000000 0*000000000000000000010000000000000000 00*00000000000000000000000000000000000 000*0000000000000000000100000000000000 0000*000000000000000000010000000000000 00000*00000000000000000000000000000000 000000*0000000000000000000100000000000 0000000*000000000000000000000000000000 00000000*00000000000000000001000000000 000000000*0000000000000000000000000000 0000000000*000000000000000000000000000 00000000000*00000000000000000000000000 000000000000*0000000000000000000000000 0000000000000*000000000000000000000000 00000000000000*00000000000000000000000 000000000000000*0000000000000000000000 0000000000000000*000000000000000000000 00000000000000000*00000000000000000000 000000000000000000*");

        public string get_s000()
        {
            return s000;
        }

        public string get_s004_1()
        {
            return s004_1;
        }

        public string get_s004_2()
        {
            return s004_2;
        }

        public string get_s005_1()
        {
            return s005_1;
        }

        public string get_s005_2()
        {
            return s005_2;
        }

        public string get_s036()
        {
            return s036;
        }
    }
}