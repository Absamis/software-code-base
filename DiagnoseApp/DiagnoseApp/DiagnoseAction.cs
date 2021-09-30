using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnoseApp
{
    class DiagnoseAction
    {
        List<DiagnoseChain> diagchain, Vdiagchain;
        IDictionary<int, int> showed;
        public int diagSection = 10;
        public DiagnoseAction()
        {
            showed = new Dictionary<int, int>();
            Vdiagchain = new List<DiagnoseChain>()
            {
                new DiagnoseChain {Text = "Troubleshooting process ends.", Chain = -1, report = true, Pchain = 20, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does the system power up?", Chain = 0, Pchain = 20, ResponseWith = "Y" },
                new DiagnoseChain {Text = "There might be power supply failre; Proceed to power supply troubleshooting?", Chain = 10, Pchain = 0, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does it show live screen?", Chain = 1, Pchain = 0, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does the monitor power on?", Chain = 2, Pchain = 1, ResponseWith = "N" },
                new DiagnoseChain {Text = "Check monitor power source. Make sure a detachable power cord is fully seated in the monitor socket.", Chain = -1, Pchain = 2, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is controls in middle of range?", Chain  = 4, Pchain = 2, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does it show blinking cursor only?", Chain = 3, Pchain = 1, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does it make string of beeps?", Chain = 5, Pchain = 3, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Adjust brghtness and contrast.", Chain = -1, Pchain = 4, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does it make string of beeps?", Chain = 5, Pchain = 4, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is the Video adapter seated properly?", Chain = 7, Pchain = 5, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Reseat video adapter properly, Secure.", Chain = -1, Pchain = 7, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the video cable secure?", Chain = 8, Pchain = 5, ResponseWith = "N" },
                new DiagnoseChain {Text = "Connect the video cable.", Chain = -1, Pchain = 8, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the RAM Seated properly?", Chain = 9, Pchain = 7, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Reseat the RAM properly on the motherboard.", Chain = -1, Pchain = 9, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the video cable damaged or pin bents?", Chain = 10, Pchain = 9, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Repair or replace video cable.", Chain = -1, Pchain = 10, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is the video cable damaged or pin bents?", Chain = 10, Pchain = 8, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is there any other adapter installed on the system?", Chain = 11, Pchain = 10, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does it show live video when the adapter is removed?", Chain = 13, Pchain = 11, ResponseWith = "Y" },
                new DiagnoseChain {Text = "There might be conflict of adapters in the system.", Chain = -1, Pchain = 13, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does it display live video when new adapter is installed?", Chain = 14, Pchain = 11, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does it display live video when new adapter is installed?", Chain = 14, Pchain = 13, ResponseWith = "N" },
                new DiagnoseChain {Text = "Try the old adapter in other systems before chucking?", Chain = -1, Pchain = 14, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "The motherboard might be faulty; Procced to motherboard troubleshooting", Chain = 30, Pchain = 14, ResponseWith = "N" }
            };
            diagchain = new List<DiagnoseChain>()
            {
                //First yes  block hierachy
                new DiagnoseChain {Text = "Troubleshooting process ends.", Chain = -1, report = true, Pchain = 10, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does the power comes on?", Chain = 0, Pchain = 10, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is their live screen display?", Chain = 1, Pchain = 0, ResponseWith = "Y" },
                new DiagnoseChain {Text = "There might be issue of video failure, Proceed to video troubleshooting?", Chain = 20, Pchain = 1, ResponseWith = "N" },
                new DiagnoseChain {Text = "Try boot again; Does it boot?", Chain = 3, Pchain = 1, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Premature power supply detected. Try a different power supply.", Chain = -1, report = true, Pchain =3, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does it give any beep sound?", Chain = 5, Pchain = 3, ResponseWith = "N" },
                new DiagnoseChain {Text = "There night be a failure in the motherboard; Proceed to motherboard troubleshooting?", Chain = 30, Pchain = 5, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Did you install any hardware earlier?", Chain = 7, Pchain = 5, ResponseWith = "N" },
                new DiagnoseChain {Text = "Remove the hardware and Test, Replace the power supply?", Chain = -1, Pchain = 7, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does the hard drive spin up?", Chain = 10, Pchain = 7, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the Adapter on the Bus bad?", Chain = 12, Pchain = 10, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Does it spin up on other lead?", Chain= 13, Pchain = 10, ResponseWith = "N" },
                new DiagnoseChain {Text = "Strip the system down to video adapter only.", Chain = -1, Pchain = 12, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is the motherboard's power on bench?", Chain = 11, Pchain = 12, ResponseWith = "N" },
                new DiagnoseChain {Text = "Replace the power Supply.", Chain = -1, Pchain = 11, ResponseWith = "N" },
                new DiagnoseChain {Text = "Either you have a short circuit in the case or a geometry problem placing an unacceptable stress on the motherboard. It's also possible that the video adapter was never seated, due the bracket position!!!", Chain = -1, Pchain = 11, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Try Drive in test PC.", Chain = -1, Pchain = 13, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Defective power supply lead or connector", Chain = -1, Pchain = 13, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is it connected to a good power source?", Chain = 2, Pchain = 0, ResponseWith = "N" },
                new DiagnoseChain {Text = "Use live outlet.", Chain = -1, Pchain = 2, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the power set 110/220 V?", Chain = 4, Pchain = 2, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Select proper voltage on rear of power supply.", Chain = -1, Pchain = 4, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Is the motherboard lead installed?", Chain = 6, Pchain = 4, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Check manual and motherboard silkscreen, Connect lead from front panel switch.", Chain = -1, Pchain = 6, report = true, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does the power switch fails?", Chain = 8, Pchain = 6, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Replace switch or subtitute front panel reset switch if available.", Chain = -1, Pchain = 8, report = true, ResponseWith = "Y" },
                new DiagnoseChain {Text = "Is the power supply, connected to the motherboard correctly?", Chain = 9, Pchain = 8, ResponseWith = "N" },
                new DiagnoseChain {Text = "Remake motherboard power supply connection.", Chain = -1, report = true, Pchain = 9, ResponseWith = "N" },
                new DiagnoseChain {Text = "Does the hard drive spin up?", Chain = 10, Pchain = 9, ResponseWith = "Y" }
            };
        }
        public string ReturnPrev(int chain)
        {
            string reply = "Troubleshooting ends report*0*0";
            if (showed.Count > 0)
            {
                foreach (KeyValuePair<int, int> elem in showed)
                {
                    //showed.Remove(elem.Key);
                    if (elem.Key == chain)
                    {
                        if (diagSection == 10)
                        {
                            foreach (DiagnoseChain elemt in diagchain)
                            {
                                if (elemt.Chain == elem.Value)
                                {
                                    reply = elemt.Text + "*" + elemt.Chain + "*" + elemt.ResponseWith;
                                    //showed.Remove(elem.Key);
                                    break;
                                }
                            }
                        }
                        else if (diagSection == 20)
                        {
                            foreach (DiagnoseChain elemt in Vdiagchain)
                            {
                                if (elemt.Chain == elem.Value)
                                {
                                    reply = elemt.Text + "*" + elemt.Chain +"*"+ elemt.ResponseWith;
                                    //Console.Write($"{elemt.Chain} {elemt.Text} {elemt.ResponseWith}");
                                    //showed.Remove(elem.Key);
                                    break;
                                }
                            }
                        }
                        showed.Remove(elem.Key);
                        break;
                    }
                }
            }
            return reply;
        }
        private string SubDecide(List<DiagnoseChain> arg1, string response, int chain)
        {
            string reply = "Troubleshooting ends report*0";
            int resp = 0;
            foreach (DiagnoseChain elem in arg1)
            {
                if (elem.ResponseWith == response && elem.Pchain == chain)
                {
                    reply = elem.Text + "*" + elem.Chain;
                    resp = 1;
                }
                if (elem.ResponseWith == response && elem.Pchain == chain && elem.report == true)
                {
                    reply = elem.Text + "report*" + elem.Chain;
                    resp = 1;
                }
                if (elem.ResponseWith == response && elem.Pchain == chain && elem.Chain % 10 == 0 && elem.Chain != 0)
                {
                    //Console.WriteLine("This is fir redirecting");
                    reply = elem.Text + "redirect*" + elem.Chain;
                    resp = 1;
                }
                if(resp == 1)
                {
                    if(showed.ContainsKey(elem.Chain))
                        showed.Remove(elem.Chain);
                    showed.Add(elem.Chain, chain);
                    break;
                }
            }
            return reply;
        }
        public string DecideWithResponse(string response, int chain) {
            string reply = "";
            if (diagSection == 10)
            {
                reply = SubDecide(diagchain, response, chain);
            }
            else if (diagSection == 20)
            {
                reply = SubDecide(Vdiagchain, response, chain);
            }
            return reply;
        }
    }
    public class DiagnoseChain
    {
        public string Text { get; set; }
        public int Chain { get; set; }
        public int Pchain { get; set; }
        public string ResponseWith { get; set; }
        public bool report { get; set; }
    }

}
