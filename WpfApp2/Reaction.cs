//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reaction
    {
        public int ID { get; set; }
        public int Sender { get; set; }
        public int Post { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Post Post1 { get; set; }
    }
}
