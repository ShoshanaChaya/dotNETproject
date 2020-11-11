using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        public override string ToString()
        {
            string dates = "Hosting unit key: " + HostingUnitKey + Environment.NewLine + "Hosting unit name: " + HostingUnitName + Environment.NewLine + "Area: " + Area + "\n" + Environment.NewLine + "Owner: " + Owner + "The dates that are occupied: ";
            bool flag = false;
            int firstDay = 0;
            int firstMonth = 0;
            for (int i = 0; i < 12; i++) // looks and emitts the booked days
            {
                for (int j = 0; j < 31; j++)
                {
                    if (Diary[i, j] == true && flag == false) // the first day of booked days
                    {
                        firstDay = j + 1;
                        firstMonth = i + 1;
                        flag = true;

                    }
                    if (Diary[i, j] == true && j != 30 && Diary[i, j + 1] == false) // last day of booked days
                    {
                        dates = dates + "First date: " + firstDay + "/" + firstMonth + "/2020 " + " lastDate: " + (j + 1) + "/" + (i + 1) + "/2020 " + Environment.NewLine;
                        flag = false;
                    }
                    if (i == 11 && j == 30) // the last day of the year
                    {
                        if (flag == true) // last day of booked days
                        {
                            dates = dates + "First date: " + firstDay + "/" + firstMonth + "/2020 " + " lastDate: " + (j + 1) + "/" + (i + 1) + "/2020 " + Environment.NewLine;
                            flag = false;
                        }
                        else if (Diary[i, j] == true) // booked for a single day
                        {
                            dates = dates + "First date: " + (j + 1) + "/" + (i + 1) + "/2020 " + " lastDate: " + (j + 1) + "/" + (i + 1) + "/2020 " + Environment.NewLine;
                            flag = false;
                        }
                    }
                }
            }
            return dates;
        }
        // properties:
        public long HostingUnitKey { get; set; }
        public string HostingUnitName { get; set; }
        public Host Owner { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        //optional. tell the XmlSerializer to name the Array Element as'Board'
        // instead of 'BoaredDto'
        [XmlArray("Diary")]
        public bool[] NewDiary
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(31); } //5 is the number of roes in the matrix
        }

        public Enum.Area Area { get; set; }
        public Enum.ResortType Type { get; set; }
        public Enum.Spooky Spooky { get; set; }
        public Enum.Pool Pool { get; set; }
        public Enum.Jacuzzi Jacuzzi { get; set; }
        public Enum.Garden Garden { get; set; }
        public Enum.ChildrensAttractions ChildrensAttractions { get; set; }
    }

}