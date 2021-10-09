using System.Threading.Tasks;
using FleetChatBotServer.Infrastructure.Commons.HttpConnection;
using FleetChatBotServer.UGProMiddleware.Dtos;

namespace FleetChatBotServer.ChatBot.Formatter.UGPro
{
    public class InferenceAnswerFormatter
    {
        private readonly InferenceTags _tag;
        private readonly string _answer;
        private readonly string _predicate;
        private readonly string _endpoint;
        private readonly IHttpConnection _httpClient;
        private IInferenceFormatter _formatter;

        public InferenceAnswerFormatter(InferenceTags tag, string answer, string predicate, string endpoint, IHttpConnection httpClient)
        {
            _tag = tag;
            _answer = answer;
            _predicate = predicate;
            _endpoint = endpoint;
            _httpClient = httpClient;
        }

        public async Task<string> GetAnswer()
        {
            HttpResquestBody body = new()
            {
                ControllerPath = string.Format(_endpoint, _predicate)
            };

            if (_tag == InferenceTags.equipment_current_state)
            {
                var webApiResult = await _httpClient.GetAsync<EquipmentRealTimeDto>(body);
                _formatter = new EquipmentCurrentStateFormatter(_answer, _predicate, webApiResult);
            }
            else if (_tag == InferenceTags.equipment_current_state_time)
            {
                var equipmentRealTime = await _httpClient.GetAsync<EquipmentRealTimeDto>(body);
                _formatter = new EquipmentCurrentStateTimeFormatter(_answer, _predicate, equipmentRealTime);
            }
            else if (_tag == InferenceTags.equipment_current_operator)
            {
                var equipmentRealTime = await _httpClient.GetAsync<OperatorDto>(body);
                _formatter = new EquipmentCurrentOperatorFormatter(_answer, _predicate, equipmentRealTime);
            }
            else if (_tag == InferenceTags.workplace_current_state)
            {
                var webApiResult = await _httpClient.GetAsync<WorkplaceRealTimeDto>(body);
                _formatter = new WorkplaceCurrentStateFormatter(_answer, _predicate, webApiResult);
            }
            else if (_tag == InferenceTags.equipment_current_state_time)
            {
                var webApiResult = await _httpClient.GetAsync<WorkplaceRealTimeDto>(body);
                _formatter = new WorkplaceCurrentStateFormatter(_answer, _predicate, webApiResult);
            }
            else // _tag == InferenceTags.undefined
            {
                _formatter = new UndefinedFormatter(_answer);
            }
            
            return _formatter.AnswerFormatted;
        }
    }
}
