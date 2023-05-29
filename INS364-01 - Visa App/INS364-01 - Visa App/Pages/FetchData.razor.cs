using System.Text.Json;
using Visa_App;

namespace INS364_01___Visa_App.Pages;

public partial class FetchData
{
    private string json = @"
    {
        ""MUN_CED"":""001"",
        ""SEQ_CED"":""0078278"",
        ""VER_CED"":""8"",
        ""NOMBRES"":""DANILO"",
        ""APELLIDO1"":""MEDINA"",
        ""APELLIDO2"":""SANCHEZ"",
        ""FECHA_NAC"":""1951-11-10 00:00:00.000000"",
        ""LUGAR_NAC"":""SAN JUAN DE LA MAGUANA"",
        ""CED_A_NUM"":""033351"",
        ""CED_A_SERI"":""012"",
        ""SEXO"":""M"",
        ""Mun"":""MARTIN PUCHI"",
        ""ciudad"":""12"",
        ""calle"":null,
        ""campo1"":null,
        ""campo2"":null,
        ""TELEFONO"":""6828785"",
        ""campo3"":""001"",
        ""campo4"":""01"",
        ""campo5"":""0755"",
        ""campo6"":""2"",
        ""campo7"":""7"",
        ""campo8"":""M"",
        ""campo9"":null,
        ""campo10"":""72"",
        ""campo11"":""1"",
        ""EST_CIVIL"":""S"",
        ""campo12"":null,
        ""campo13"":null,
        ""campo14"":""2008-12-31 00:00:00.000000"",
        ""campo15"":""0"",
        ""campo16"":null,
        ""campo17"":null,
        ""campo18"":null,
        ""campo19"":""T"",
        ""campo20"":null,
        ""campo21"":null,
        ""campo22"":null,
        ""campo23"":null,
        ""campo24"":null,
        ""campo25"":null
    }";
    private SolicitudDto persona = new();
    private List<SolicitudDto> Solicitudes = new();
    private string selectedSearchValue = string.Empty;
    private bool isNewRecord = true;

    protected override async Task OnInitializedAsync()
    {
        var solicitud = JsonSerializer.Deserialize<SolicitudDto>(json) ?? throw new Exception("No se pudo crear el objeto");
        Solicitudes.Add(solicitud);
        await Task.CompletedTask;
    }

    private void IdSearched(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            selectedSearchValue = string.Empty;
            persona = new();
            isNewRecord = true;
            return;
        }

        var idSplitted = id.Split('-');

        selectedSearchValue = id;
        var solicitud = Solicitudes.Where(x => x.MUN_CED == idSplitted[0] || x.SEQ_CED == idSplitted[1] || x.VER_CED == idSplitted[2]).FirstOrDefault();

        if (solicitud is null)
        {
            return;
        }

        isNewRecord = false;
        persona = solicitud;
    }

    private bool ValidatePerson()
    {
        if (string.IsNullOrWhiteSpace(persona.NOMBRES))
            return false;
        if (string.IsNullOrWhiteSpace(persona.APELLIDO1))
            return false;
        if (string.IsNullOrWhiteSpace(persona.APELLIDO2))
            return false;
        if (string.IsNullOrWhiteSpace(persona.SEXO))
            return false;
        if (string.IsNullOrWhiteSpace(persona.FECHA_NAC))
            return false;
        if (string.IsNullOrWhiteSpace(persona.TELEFONO))
            return false;
        if (string.IsNullOrWhiteSpace(persona.LUGAR_NAC))
            return false;

        return true;
    }

    private void OnButtonClicked()
    {
        if (!ValidatePerson())
        {
            return;
        }

        Solicitudes.Add(persona);
        persona = new();
        isNewRecord = true;
    }

    private void NewRecord()
    {
        persona = new();
        isNewRecord = true;
    }
}