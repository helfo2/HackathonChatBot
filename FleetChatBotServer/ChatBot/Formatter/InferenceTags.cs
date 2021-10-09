namespace FleetChatBotServer.ChatBot.Formatter
{
    public enum InferenceTags
    {
        undefined = 0,
        greeting = 1,
        goodbye = 2,
        thanks = 3,
        about = 4,
        name = 5,
        help = 6,
        equipment_current_state = 7, // given equipment name get the current state
        equipment_current_operator = 8, // given equipment name get the current operator (name and registration)
        equipment_current_state_time = 9,// given equipment name get the current state duration
        workplace_current_state = 10, // given a workplace name get the current state
        workplace_current_state_time = 11 // given a workplace name get the current state duration
    }
}
