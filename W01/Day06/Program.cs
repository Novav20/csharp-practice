Console.WriteLine("Day 06's Challenge: Digital Twin State Transition");

PumpState state = new("PUMP-205", "Normal", 88.0, 1450);

StateTransitionService stateTransitionService = new();

var (assetId, currentState, currentTemperature, currentRpm) = stateTransitionService.ApplyTelemetry(state, 105.5, null);

Console.WriteLine($"Pump {assetId} current state: {currentState}; Temperature = {currentTemperature} °C; RPM = {currentRpm} rpm.");

// 1. Create an initial state
PumpState state1 = new("PUMP-205", "Normal", 88.0, 1450);

// 2. Apply telemetry that results in the EXACT same values
PumpState state2 = stateTransitionService.ApplyTelemetry(state1, 88.0, 1450);

// 3. Verify reference equality
bool isSameReference = ReferenceEquals(state1, state2);
Console.WriteLine($"Are they the same reference? {isSameReference}"); // This will print False

// 4. Verify value equality (Records override Equals to compare values)
bool areValuesEqual = state1 == state2;
Console.WriteLine($"Do they have the same values? {areValuesEqual}"); // This will print True