Architecture & Design Patterns
1. Singleton Pattern
•	Class: ElevatorSystem
•	Purpose: Ensures a single, globally accessible instance of the elevator system, managing all elevators and floors.
2. Strategy Pattern
•	Classes: IElevatorCarStrategy, AllFloorsElevatorStrategy, OddEvenElevatorCarStrategy
•	Purpose: Allows dynamic assignment of different scheduling and movement algorithms to elevator cars. Each car can use a different strategy for handling requests, making the system flexible and extensible.
3. Factory Pattern
