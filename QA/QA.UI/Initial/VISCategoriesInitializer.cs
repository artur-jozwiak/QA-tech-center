using QA.DataAccess;
using QA.Domain.Models;

namespace QA.UI.Initial
{
    public static class VISCategoriesInitializer
    {
        public static DefectCategory Ubytki;
        public static DefectCategory CiałaObce;
        public static DefectCategory Trzy;
        public static DefectCategory DefektyPowlekania;
        public static DefectCategory KJ;
        public static DefectCategory Obróbka;
        public static DefectCategory Inne;
        public static List<DefectCategory> InspectionCategories;
        public static List<Defect> InspectionElements;

        static VISCategoriesInitializer()
        {
            Ubytki = new DefectCategory { Number = 1, Name = "Ubytki" };
            Ubytki.Defects = new List<Defect>
            {
                new Defect {Name = "Ubytki na krawędzi skrawającej", Symbol ="1A", DefectCategory = Ubytki  },
                new Defect {Name = "Ubytki z prasowania (inne niż klejenie)", Symbol ="1B", DefectCategory =  Ubytki},
                new Defect {Name = "Ubytki za spiekania / zanieczyszczona masa", Symbol ="1C", DefectCategory =  Ubytki },
                new Defect {Name = "Ubytki po pokryciu", Symbol ="1D", DefectCategory = Ubytki  },
                new Defect {Name = "'Niewyraźny' łamacz wióra", Symbol ="1E", DefectCategory = Ubytki},
                new Defect {Name = "Uszkodzone w transporcie/rozładunku / załadunku", Symbol ="1F", DefectCategory = Ubytki },
                new Defect {Name = "Klejenie", Symbol ="1G", DefectCategory =  Ubytki  }
            };

            CiałaObce = new DefectCategory { Number = 2, Name = "Ciała Obce" };
            CiałaObce.Defects = new List<Defect>
            {
                new Defect {Name = "Pozostałości gratu w łamaczu wióra", Symbol ="2A", DefectCategory = CiałaObce  },
                new Defect {Name = "Grat na krawędzi / w otworze", Symbol ="2B", DefectCategory = CiałaObce },
                new Defect {Name = "Naddatek materiału - nadbudowanie", Symbol ="2C", DefectCategory = CiałaObce  },
                new Defect {Name = "Pęcherze, nakłucia, zaprószenia", Symbol ="2D", DefectCategory = CiałaObce},
            };

            Trzy = new DefectCategory { Number = 3, Name = "" };
            Trzy.Defects = new List<Defect>
            {
                new Defect {Name = "Pęknięcia", Symbol ="3A", DefectCategory =  Trzy },
            };

            DefektyPowlekania = new DefectCategory { Number = 5, Name = "Defekty powlekania" };
            DefektyPowlekania.Defects = new List<Defect>
            {
                new Defect {Name = "Kurz PVD", Symbol ="5A", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Nieobracający się rod na powlekaniu PVD", Symbol ="5B", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Płytka źle zorientowana na rodzie", Symbol ="5C", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Brudne / niedoczyszczone", Symbol ="5D", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Arc", Symbol ="5E", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Defekty z awari pokrywania", Symbol ="5F", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Kropla CVD", Symbol ="5G", DefectCategory = DefektyPowlekania },
                new Defect {Name = "Łuszcząca się powłoka", Symbol ="5H", DefectCategory = DefektyPowlekania },
            };

            KJ = new DefectCategory { Number = 7, Name = "KJ" };
            KJ.Defects = new List<Defect>
            {
                new Defect {Name = "Próbka dla KJ", Symbol ="7A", DefectCategory = KJ },
                new Defect {Name = "Testy", Symbol ="7B", DefectCategory = KJ },
                new Defect {Name = "Prametry Geometryczne poza spec", Symbol ="7C", DefectCategory = KJ },
                new Defect {Name = "Materiał: Magnetyczne, Mikrostruktura, ETA faza", Symbol ="7D", DefectCategory = KJ },
                new Defect {Name = "Powłoka: grubość powłoki, adhezja", Symbol ="7E", DefectCategory = KJ},
                new Defect {Name = "Deformacja", Symbol ="7F", DefectCategory = KJ },
            };

            Obróbka = new DefectCategory { Number = 8, Name = "Obróbka" };
            Obróbka.Defects = new List<Defect>
            {
                new Defect {Name = "Niedoszlifowania", Symbol ="8E", DefectCategory = Obróbka },
                new Defect {Name = "Nadszlifowana krawędź, zdeformowany  promień", Symbol ="8F", DefectCategory = Obróbka },
                new Defect {Name = "Niedopiaskowane / Nadpiaskowanie", Symbol ="8G", DefectCategory = Obróbka },
            };

            Inne = new DefectCategory { Number = 9, Name = "Inne" };
            Inne.Defects = new List<Defect>
            {
                               new Defect {Name = "BRAK SZTUK", Symbol ="9A", DefectCategory = Inne },
                new Defect {Name = "BARWA - Plamy/Zabrudzenia", Symbol ="9B", DefectCategory = Inne },
                new Defect {Name = "inne", Symbol ="9X", DefectCategory = Inne },
            };

            InspectionCategories = new List<DefectCategory>
            {
                Ubytki, CiałaObce, Trzy, DefektyPowlekania, KJ, Obróbka, Inne
            };
        }

        public static void SeedData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<QAContext>();

            if (!context.DefectCategories.Any() && !context.Defects.Any())
            {
                context.DefectCategories.AddRange(InspectionCategories);
                context.SaveChanges();
            }
        }
    }
}
