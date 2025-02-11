using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Portofolio.Models.Order.Request
{
    public class SendOrder_DTO
    {
        [JsonPropertyName("step1")]
        public StepOne_DTO StepOne { get; set; }

        [JsonPropertyName("step2")]
        public StepTwo_DTO StepTwo { get; set; }

        [JsonPropertyName("step3")]
        public StepThree_DTO StepThree { get; set; }

        [JsonPropertyName("step4")]
        public StepFour_DTO StepFour { get; set; }

        public SendOrder_DTO()
        {
            StepOne = new StepOne_DTO();
            StepTwo = new StepTwo_DTO();
            StepThree = new StepThree_DTO();
            StepFour = new StepFour_DTO();
        }
    }
}
