using DavyKager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public class AllmanSak : Spelsak
    {
        public AllmanSak(string namn, string beskrivning, bool kanplockasupp = true)
            : base(namn, beskrivning, kanplockasupp)
        {
            
        }

        public override void Anvand(Spelare s)
        {
            // En enkel standard-feedback för JAWS så länge
            Tolk.Output($"Du undersöker {this.Namn}. Det verkar inte gå att göra så mycket med den just nu.");
        }
    }
}
