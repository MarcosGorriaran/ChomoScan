using APILibraryDaltonismo.Controllers.DAO;
using APILibraryDaltonismo.Model;
using APILibraryDaltonismo.Model.DTO;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System;

namespace APILibraryDaltonismo.Controllers
{
    public class PatientController : Controller, ICreate<Patient>
    {
        const string CheckLoginPath = "CheckLogin";
        const string AddPatientPath = "AddPatient";
        const string GetPatientSessionPath = "GetPatientSessions";
        public PatientController(HttpClient httpData) : base(httpData) { }
        public ResponseDTO<Patient> CheckLogin(Patient checkPatientInfo)
        {
            return CheckLoginRequest(checkPatientInfo).GetAwaiter().GetResult();
        }

        public void Create(Patient info)
        {
            AddPatientRequest(info);
        }

        public void Create(IEnumerable<Patient> infoList)
        {
            foreach(Patient patient in infoList)
            {
                Create(patient);
            }
        }

        public async Task<ResponseDTO<Patient>> CheckLoginRequest(Patient patient)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(CheckLoginPath, patient);
            response.EnsureSuccessStatusCode();
            string responseDTO;
            responseDTO =  await response.Content.ReadAsStringAsync();
            ResponseDTO<Patient> result = JsonSerializer.Deserialize<ResponseDTO<Patient>>(responseDTO, serializerOptions);

            return result;
        }
        public async Task<ResponseDTO<object>> AddPatientRequest(Patient patient)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(AddPatientPath, patient).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ResponseDTO<object>>(await response.Content.ReadAsStringAsync(),serializerOptions);
        }
        public async Task<ResponseDTO<IEnumerable<Session>>> GetPatientSessionsRequest(Patient patient)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(GetPatientSessionPath, patient).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<ResponseDTO<IEnumerable<Session>>>(await response.Content.ReadAsStringAsync(), serializerOptions);
        }
    }
}
