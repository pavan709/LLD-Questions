# Vending Machine System Design

## Requirements

1. The vending machine should support multiple products, each with different prices and quantities.
2. The machine should accept coins and notes of various denominations.
3. The machine should dispense the selected product(s) and return change if necessary.
4. The machine should keep track of available products and their quantities.
5. The machine should handle multiple transactions concurrently and ensure data consistency.
6. The machine should provide an interface for restocking products and collecting money.
7. The machine should handle exceptional scenarios, such as insufficient funds or out-of-stock products.

[Solution](#-solution-context)

---

## Architecture & Design Patterns

1. **Singleton Pattern**  
   - **Class:** VendingMachine  
   - **Purpose:** Ensures a single, globally accessible instance of the vending machine, maintaining consistent state and inventory throughout the application.

2. **State Pattern**  
   - **Classes:** IState, IdleState, ItemSelectionState, MoneyInsertionState, ItemsDispatchState  
   - **Purpose:** Encapsulates the various operational states of the vending machine, allowing state-specific behavior and transitions. This makes the system robust, extensible, and easy to maintain.

3. **Strategy Pattern (Potential/Implicit)**  
   - **Where:** Payment handling and item selection logic can be extended to use different strategies for accepting payment or selecting items.  
   - **Purpose:** Allows for future extensibility in payment and selection logic without modifying the core vending machine code.

4. **Factory Pattern (Potential/Optional)**  
   - **Where:** Could be used for creating items or state objects, though not explicitly implemented in the current codebase.  
   - **Purpose:** Would further decouple object creation from business logic, making the system more modular.

---

## Core Components

- **VendingMachine:**  
  Singleton class that manages product inventory, coin/note stock, and delegates user actions to the current state.

- **Item:**  
  Represents a product in the vending machine, with properties for name, price, and quantity.

- **IState & State Implementations:**  
  Define the allowed actions and transitions for each phase of the vending machine's operation (Idle, Selecting Items, Inserting Money, Dispatching Items).

- **Coin/Note Enums:**  
  Enumerations for supported coin and note denominations.

---

## Flow & Logic

### Initialization

- The vending machine is initialized as a singleton.
- Admin functions are used to stock the machine with 20 different products and load coins/notes of all denominations.

### Order Simulation

- Multiple orders are simulated, each requesting a variety of products and paying with different combinations of coins and notes.
- The system checks for sufficient funds and available stock before dispensing items.
- Change is refunded if the payment exceeds the total cost.
- Orders with insufficient funds or excessive quantity requests are handled gracefully.

### State Management

- The vending machine transitions through various states (Idle, Selecting Item, Inserting Money, Dispatching Items) to mimic real-world operation.
- Each state restricts or enables certain actions, enforcing correct usage flow.

---

## Extensibility

- **Add More Products:** Easily add more items by updating the admin section in the test file.
- **Add More States:** Implement new states for additional vending machine features (e.g., Maintenance, OutOfOrder).
- **Payment Methods:** Extend payment logic to support cards, mobile payments, etc.
- **UI Integration:** The core logic is decoupled from the UI, making it suitable for integration with web, desktop, or embedded interfaces.

---

## Concurrency

- The current design is single-threaded, but the singleton and state patterns provide a solid foundation for future thread-safe extensions.

---

## Summary

This project demonstrates a robust, extensible, and maintainable approach to vending machine system design, leveraging multiple design patterns (Singleton, State, and potential Strategy/Factory) to ensure clarity, scalability, and real-world applicability.

---
