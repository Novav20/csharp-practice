public class StateTransitionService
{
    public PumpState ApplyTelemetry(PumpState currentState, double? newTemperature, int? newRpm)
    {
        var finalTemperature = newTemperature ?? currentState.Temperature;
        
        var finalRpm = newRpm ?? currentState.Rpm;
        
        if (currentState.Temperature == finalTemperature && currentState.Rpm == finalRpm)
        {
            return currentState;
        }
        var newPumpState = currentState with
        {
            Temperature = finalTemperature,
            Rpm = finalRpm,
            Status = finalTemperature > 100.0 ? "Overheating" : currentState.Status
        };
        return newPumpState;
    }
}