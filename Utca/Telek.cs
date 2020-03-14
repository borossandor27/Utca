using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utca
{
    class Telek
    {
        int oldal;      //-- a telek a páros(0) vagy a páratlan(1) oldalán van az utcának;
        int szelesseg;  //-- a telek szélességét adja meg méterben(egész szám, értéke 8 és 20 között lehet);
        char kerites;   //-- 
        int hazszam;
        static int paros = 2;  //-- kezdő házszám a páros oldalon
        static int paratlan = 1;   //-- kezdő házszám a páratlan oldalon

        public int Oldal { get => oldal; set => oldal = value; }
        public int Szelesseg { get => szelesseg; set => szelesseg = value; }
        public char Kerites { get => kerites; set => kerites = value; }
        public int Hazszam { get => hazszam; set => hazszam = value; }

        public Telek(string[] sor)
        {
            this.oldal = int.Parse(sor[0].Trim());
            if (oldal == Program.PAROS)
            {
                this.hazszam = paros;
                paros += 2;
            }
            else
            {
                this.hazszam = paratlan;
                paratlan += 2;
            }
            this.szelesseg = int.Parse(sor[1].Trim());
            this.kerites = sor[2].Trim()[0];
        }
    }
}
