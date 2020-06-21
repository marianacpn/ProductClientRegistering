using System.ComponentModel.DataAnnotations;

namespace Shared.Enums
{
    public enum ClientStatusEnum
    {
        [Display(Name = "Prata")]
        silver = 1,

        [Display(Name = "Ouro")]
        gold = 2,

        [Display(Name = "Platina")]
        platinum = 3,

        [Display(Name = "Diamante")]
        diamond = 4
    }
}
