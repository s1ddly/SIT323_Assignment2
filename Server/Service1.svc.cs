using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Heuristic1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, pleprocsase select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private double Consumption(double freq, List<int> coefs)
        {
            return (Convert.ToDouble(coefs[2]) * freq * freq) + (Convert.ToDouble(coefs[1]) * freq) + Convert.ToDouble(coefs[0]);
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string Greedyout(string instr)
        {

            Allocation inp = new Allocation();
            if (!inp.init(instr))
            {
                return inp.errs;
            }

            string[] Allocs = new string[inp.Procs.Count()];
            double[] tots = new double[inp.Procs.Count()];
            double energy = 0.0;
            string outstr = "Greedy Algorithm\r\n";

            try
            {
                for (int i = 0; i < inp.Tasks.Count(); i++)
                {
                    bool chk = true;
                    for (int c = 0; c < tots.Length; c++)
                    {
                        double calc = (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                        if(calc + tots[c] <= inp.MDuration && chk)
                        {
                            tots[c] = calc + tots[c];
                            Allocs[c] = Allocs[c] + ",1";
                            energy += Consumption(inp.Procs[c + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                            chk = false;
                        }
                        else
                        {
                            Allocs[c] = Allocs[c] + ",0";
                        }
                    }
                    if (chk)
                    {
                        int row = Array.IndexOf(tots, tots.Min());
                        tots[row] = (inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc) + tots[row];
                        Allocs[row] = Allocs[row].Remove(Allocs[row].Length - 1, 1) + "1";
                        energy += Consumption(inp.Procs[row + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc);
                        //outstr += "Task: " + Convert.ToString(i + 1) + ", Runtime: " + inp.Tasks[i + 1] + " Could not be allocated using this algorithm\n";
                    }
                }



                outstr += "Allocation ID = 1, Time = " + tots.Max() + ", Energy = " + Convert.ToString(energy) + "\r\n";

                for(int i = 0; i < Allocs.Length; i++)
                {
                    outstr += Allocs[i].Substring(1) + "\r\n";
                }
                return outstr;
            }
            catch (Exception ex)
            {
                inp.errs += ex.ToString();
                return inp.errs;
            }

        }

        public string Heuristicout(string instr)
        {
            Allocation inp = new Allocation();
            if (!inp.init(instr))
            {
                return inp.errs;
            }

            string[] Allocs = new string[inp.Procs.Count()];
            double[] tots = new double[inp.Procs.Count()];
            double energy = 0.0;
            string outstr = "Heuristic Algorithm\r\n";

            try
            {
                for (int i = inp.Tasks.Count() - 1; i >= 0; i--)
                {
                    bool chk = true;
                    for (int c = tots.Length - 1; c >= 0; c--)
                    {
                        double calc = (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                        if (calc + tots[c] <= inp.MDuration && chk)
                        {
                            tots[c] = calc + tots[c];
                            Allocs[c] = Allocs[c] + ",1";
                            //Allocs[c] = Allocs[c] + "," + Convert.ToString(calc);
                            energy = energy + Consumption(inp.Procs[c + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                            chk = false;
                        }
                        else
                        {
                            Allocs[c] = Allocs[c] + ",0";
                        }
                    }
                    if (chk)
                    {
                        int row = Array.IndexOf(tots, tots.Min());
                        tots[row] = inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc + tots[row];
                        Allocs[row] = Allocs[row].Remove(Allocs[row].Length - 1, 1) + "1";
                        energy = energy + Consumption(inp.Procs[row + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc);
                        //outstr += "Task: " + Convert.ToString(i + 1) + ", Runtime: " + inp.Tasks[i + 1] + " Could not be allocated using this algorithm\n";
                    }
                }



                outstr += "Allocation ID = 2, Time = " + tots.Max() + ", Energy = " + Convert.ToString(energy) + "\r\n";

                for (int i = 0; i < Allocs.Length; i++)
                {
                    outstr += Allocs[i].Substring(1) + "\r\n";
                }
                return outstr;
            }
            catch (Exception ex)
            {
                inp.errs += ex.ToString();
                return inp.errs;
            }
        }

        public string Heuristicout2(string instr)
        {
            Allocation inp = new Allocation();
            if (!inp.init(instr))
            {
                return inp.errs;
            }

            string[] Allocs = new string[inp.Procs.Count()];
            double[] tots = new double[inp.Procs.Count()];
            double energy = 0.0;
            string outstr = "Heuristic Algorithm\r\n";

            try
            {
                for (int i = inp.Tasks.Count() - 1; i >= 0; i--)
                {
                    bool chk = true;
                    for (int c = 0; c < tots.Length; c++)
                    {
                        double calc = (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                        if (calc + tots[c] <= inp.MDuration && chk)
                        {
                            tots[c] = calc + tots[c];
                            Allocs[c] = Allocs[c] + ",1";
                            //Allocs[c] = Allocs[c] + "," + Convert.ToString(calc);
                            energy = energy + Consumption(inp.Procs[c + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[c + 1] * inp.RProc);
                            chk = false;
                        }
                        else
                        {
                            Allocs[c] = Allocs[c] + ",0";
                        }
                    }
                    if (chk)
                    {
                        int row = Array.IndexOf(tots, tots.Min());
                        tots[row] = inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc + tots[row];
                        Allocs[row] = Allocs[row].Remove(Allocs[row].Length - 1, 1) + "1";
                        energy = energy + Consumption(inp.Procs[row + 1], inp.Coefs) * (inp.Tasks[i + 1] / inp.Procs[row + 1] * inp.RProc);
                        //outstr += "Task: " + Convert.ToString(i + 1) + ", Runtime: " + inp.Tasks[i + 1] + " Could not be allocated using this algorithm\n";
                    }
                }



                outstr += "Allocation ID = 3, Time = " + tots.Max() + ", Energy = " + Convert.ToString(energy) + "\r\n";

                for (int i = 0; i < Allocs.Length; i++)
                {
                    outstr += Allocs[i].Substring(1) + "\r\n";
                }
                return outstr;
            }
            catch (Exception ex)
            {
                inp.errs += ex.ToString();
                return inp.errs;
            }
        }

        /*public string Energy(string instr)
        {
            Allocation inp = new Allocation();
            if (!inp.init(instr))
            {
                return inp.errs;
            }

            string[] Allocs = new string[inp.Procs.Count()];
            double[] tots = new double[inp.Procs.Count()];
            double energy = 0.0;
            string outstr = "Effiency Optimized\r\n";

            return outstr;
        }*/

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }

    public class Allocation
    {
        public string errs { get; set; }
        public double MDuration { get; set; }
        public int PTasks { get; set; }
        public int PProcs { get; set; }
        public double RProc { get; set; }
        public Dictionary<int, double> Tasks = new Dictionary<int, double>();
        public Dictionary<int, double> Procs = new Dictionary<int, double>();
        public List<int> Coefs = new List<int>();
        public List<string> lines = new List<string>();
        public bool init(string instr)
        {
            bool Taskswitch = false;
            bool Procswitch = false;
            bool Coefswitch = false;
            int i = 0;
            try
            {
                lines = new List<string>(instr.Replace("\r", "").Split(new[] { "\n" }, StringSplitOptions.None));

                for (i = 0; i < lines.Count(); i++)
                {
                    if (lines[i].Contains("PROGRAM-MAXIMUM-DURATION,"))
                    {
                        MDuration = Convert.ToDouble(lines[i].Split(new[] { "," }, StringSplitOptions.None)[1]);
                    }

                    if (lines[i].Contains("PROGRAM-TASKS,"))
                    {
                        PTasks = Convert.ToInt32(lines[i].Split(new[] { "," }, StringSplitOptions.None)[1]);
                    }

                    if (lines[i].Contains("PROGRAM-PROCESSORS,"))
                    {
                        PProcs = Convert.ToInt32(lines[i].Split(new[] { "," }, StringSplitOptions.None)[1]);
                    }

                    if (lines[i].Contains("RUNTIME-REFERENCE-FREQUENCY,"))
                    {
                        RProc = Convert.ToDouble(lines[i].Split(new[] { "," }, StringSplitOptions.None)[1]);
                    }

                    if (Taskswitch)
                    {
                        if (!lines[i].Contains(","))
                        {
                            Taskswitch = false;
                        }
                        else
                        {
                            Tasks.Add(Convert.ToInt32(lines[i].Split(',')[0]), Convert.ToDouble(lines[i].Split(',')[1]));
                        }
                    }

                    if (Procswitch)
                    {
                        if (!lines[i].Contains(","))
                        {
                            Procswitch = false;
                        }
                        else
                        {
                            Procs.Add(Convert.ToInt32(lines[i].Split(',')[0]), Convert.ToDouble(lines[i].Split(',')[1]));
                        }
                    }

                    if (Coefswitch)
                    {
                        if (!lines[i].Contains(","))
                        {
                            Coefswitch = false;
                        }
                        else
                        {
                            Coefs.Add(Convert.ToInt32(lines[i].Split(',')[1]));
                        }
                    }

                    if (lines[i].Contains("TASK-ID,RUNTIME"))
                    {
                        Taskswitch = true;
                    }

                    if (lines[i].Contains("PROCESSOR-ID,FREQUENCY"))
                    {
                        Procswitch = true;
                    }

                    if (lines[i].Contains("COEFFICIENT-ID,VALUE"))
                    {
                        Coefswitch = true;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                errs = Convert.ToString(i);
                errs += ex.ToString();
                errs += Convert.ToString(MDuration);
                errs += Convert.ToString(PTasks);
                errs += Convert.ToString(PProcs);
                errs += Convert.ToString(Tasks);
                errs += Convert.ToString(Procs);
                errs += Convert.ToString(Coefs);
                errs += Convert.ToString(lines);
                return false;
            }
        }
    }
}
