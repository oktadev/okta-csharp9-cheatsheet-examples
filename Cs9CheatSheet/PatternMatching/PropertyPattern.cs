namespace Cs9CheatSheet.PatternMatching.PropertyPattern
{
    class PropertyPattern
    {
        enum Companies { C1, C2, C3 };
        enum Zones { Z1, Z2, Z3 };

        class ZonalClient
        {
            public Companies Company { get; }
            public Zones Zone { get; }
            public int Purchases { get; }
        }

        class DiscountApplier
        {
            int Traditional(ZonalClient client)
            {
                if (client.Purchases >= 500)
                    return 50;
                else if (client.Purchases >= 200 && client.Purchases < 500)
                    return 30;
                else if (client.Company == Companies.C1 
                         && (client.Zone == Zones.Z1 || client.Zone == Zones.Z3) 
                         && client.Purchases >= 150)
                    return 25;
                else if (client.Purchases >= 150 
                         || (client.Company == Companies.C2 && client.Purchases >= 100))
                    return 20;
                else if ((client.Zone == Zones.Z2 && client.Purchases >= 50) 
                         || ((client.Company == Companies.C2 || client.Company == Companies.C3) 
                              && client.Zone == Zones.Z3))
                    return 15;
                else if (client.Purchases >= 25)
                    return 5;
                else return 0;
            }

            int PropertyPattern(ZonalClient client) => client switch
            {
                { Purchases: >= 500 } => 50,
                { Purchases: >= 200 and < 500 } => 30,
                { Company: Companies.C1, Zone: Zones.Z1 or Zones.Z3, Purchases: >= 150 } => 25,
                { Purchases: >= 150 } or { Company: Companies.C2, Purchases: >= 100 } => 20,
                { Zone: Zones.Z2, Purchases: >= 50 } or { Company: Companies.C2 or Companies.C3, Zone: Zones.Z3 } => 15,
                { Purchases: >= 25 } => 5,
                _ => 0
            };
        }
    }
}
