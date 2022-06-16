using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR_Overlay.Enums
{
    public enum SARRarety : int
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
    }

    public enum SARWeapon : int
    {
        Pistol = 0,
        DualPistols = 1,
        Magnum = 2,
        Deagle = 3,
        SilencedPistol = 4,
        Shotgun = 5,
        JAG7 = 6,
        SMG = 7,
        ThomasGun = 8,
        AK = 9,
        M16 = 10,
        DognasDartGun = 11,
        HuntingRifle = 12,
        Sniper = 13,
        Minigun = 14,
        Bow = 15,
        SparrowLauncher = 16,
        BCG = 17,
    }

    public enum SARUtil : int
    {
        ClawBoots = 0,
        BananaForker = 1,
        NinjaBooties = 2,
        SkunkGasSnorkel = 3,
        Cupgrade = 4,
        ImposibleTape = 5,
        SuperBandoiler = 6,
    }

    public enum SARAmmo : int
    {
        LittleBullets = 0,
        Shells = 1,
        BigBullets = 2,
        SniperBullets = 3,
        SpecialtyAmmo = 4,
    }
}
