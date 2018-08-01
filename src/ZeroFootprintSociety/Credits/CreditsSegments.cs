
using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.Credits
{
    public static class CreditColors
    {
        
        public static readonly Color Backend = Color.FromNonPremultiplied(120, 80, 255, 255);
        public static readonly Color Project = Color.FromNonPremultiplied(50, 255, 50, 255);
        public static readonly Color Frontend = Color.FromNonPremultiplied(255, 80, 80, 255);
        public static readonly Color Gameplay = Color.FromNonPremultiplied(50, 160, 255, 255);
    }
    
    public static class CreditNames
    {
        public const string Noah = "Noah Reinagel";
        public const string Silas = "Silas Reinagel";
        public const string Felix = "Felix Dion-Robidoux";
        public const string Federico = "Federico Bellucci";
        public const string Tim = "Tim Reinagel";
        public const string Herbert = "Herbert Losloso";
        public const string Gordy = "Gordy Keene";
    }
    
    public sealed class ProjectManagerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Project;
        public override string Role => "Project Manager";
        public override string Name => CreditNames.Silas;
    }

    public sealed class LeadGameDesignerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Gameplay;
        public override string Role => "Lead Game Designer";
        public override string Name => CreditNames.Silas;
    }

    public sealed class UiDesignerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Frontend;
        public override string Role => "UI Designer";
        public override string Name => CreditNames.Silas;
    }
    
    public sealed class AudioCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Frontend;
        public override string Role => "Sound / Music";
        public override string Name => CreditNames.Silas;
    }
    
    public sealed class LeadArtistCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Frontend;
        public override string Role => "Lead Artist";
        public override string Name => CreditNames.Federico;
    }

    public sealed class EnvironmentArtCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Frontend;
        public override string Role => "Environment Art";
        public override string Name => CreditNames.Federico;
    }

    public sealed class LevelDesigner1Credit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Gameplay;
        public override string Role => "Level Designer";
        public override string Name => CreditNames.Federico;
    }

    public sealed class LevelDesigner2Credit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Gameplay;
        public override string Role => "Level Designer";
        public override string Name => CreditNames.Noah;
    }
    
    public sealed class LeadProgrammerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Backend;
        public override string Role => "Lead Programmer";
        public override string Name => CreditNames.Noah;
    }

    public sealed class GameplayProgrammerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Backend;
        public override string Role => "Gameplay / Engine Programmer";
        public override string Name => CreditNames.Noah;
    }
    
    public sealed class ProgrammerCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Backend;
        public override string Role => "Programmer";
        public override string Name => CreditNames.Felix;
    }

    public sealed class AlgorithmOptimizer : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Backend;
        public override string Role => "Algorithm Optimizer";
        public override string Name => CreditNames.Tim;
    }

    public sealed class CharacterArtistCredit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Frontend;
        public override string Role => "Character Artist";
        public override string Name => CreditNames.Herbert;
    }

    public sealed class Tester1Credit : BasicJamCreditSegment
    {
        public override Color RoleColor => CreditColors.Project;
        public override string Role => "Game Tester";
        public override string Name => CreditNames.Gordy;
    }
}
