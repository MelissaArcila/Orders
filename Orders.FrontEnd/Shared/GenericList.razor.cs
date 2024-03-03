using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace Orders.FrontEnd.Shared
{
    public partial class GenericList<Titem> ///componente que va a permitir genera listas de diferentes cosas
    {
        [Parameter] public RenderFragment? Loading { get; set; } //que voy a mostrar cuando este cargando - opcional
        [Parameter] public RenderFragment? NoRecords { get; set; } //que voyu a mostrar cuando no haya paises - opcional
        [EditorRequired, Parameter] public RenderFragment Body { get; set; } = null!;// cuerpo - obligatorio
        [EditorRequired, Parameter] public List<Titem> MyList { get; set; } = null!;//lista de productos - obligatorio
    }
}