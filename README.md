
### It Contains Basic Gameplay element/loop for testing purposes but was initially focus on creating following Systems:

## 1. Inventory System
- Player can store either predefined resources like Wood, Stone, Food, Metal etc.

<img width="524" height="177" alt="image" src="https://github.com/user-attachments/assets/1d8ce8c1-4ece-43c9-9b1f-8b1a7cb4392c" />

or can store Equipments like Sword, Shield, Armor etc

<img width="511" height="426" alt="image" src="https://github.com/user-attachments/assets/67e8d0fd-34ae-48df-988a-19d43099986f" />

With the ability to use/equip directly from inventory.

## 2. Equipment System
- Using Scriptable Objects, the system can easily create new equipment which also helps in loading equipments data in Inventory to Players Equipment to Chest easily.
- Each equipment cotains Stats like : Attack Power, Resistance, Armor etc directly linked to players stats

<img width="396" height="823" alt="image" src="https://github.com/user-attachments/assets/4ef241df-77de-497c-8749-92075445eb17" />

## 3. Basic Enemy AI
- Moves Around Area and Targets Player when in range. 
- Starts chasing and attacks when in range. (Uses RayCast to detect player)

  <img width="388" height="199" alt="Screenshot 2025-11-12 132728" src="https://github.com/user-attachments/assets/f0381c0e-5319-4488-b782-ba563538ab75" />

  <img width="885" height="524" alt="Screenshot 2025-11-12 132717" src="https://github.com/user-attachments/assets/c1033603-13a9-4863-ab17-7edfb2e4dc30" />

## 4. Player Interaction Sytem
- Player can interact with IInteractable Objects in Range.
- If multiple objects are interactable within range, it interact with closest one.
- Each IInteractable Object has their own message to display and is displayed at the bottom of the screen.

### a. Interaction with Chest:

<img width="495" height="397" alt="image" src="https://github.com/user-attachments/assets/414a160a-d571-4f1b-a71e-ca0d86c1dff2" />

### b. Interaction with Food Resource:

<img width="455" height="357" alt="image" src="https://github.com/user-attachments/assets/277ab2f6-6d22-4d77-872d-c4a50bcf8d00" />

## 5. Player Combact  
- Player has 2 different Attack Pattern i.e Light and Heavy
- Both attack can be completly customized according to users needs like: attack Cooldown, Power, Type of damage it deals etc...

### a. Light Attack

<img width="436" height="314" alt="Screenshot 2025-11-12 133734" src="https://github.com/user-attachments/assets/4421cdf8-a98f-4b66-a8c9-4b931daaf8c8" />

### b. Heavy Attack

<img width="365" height="241" alt="Screenshot 2025-11-12 133751" src="https://github.com/user-attachments/assets/d868f42b-c6c9-4593-8b2a-750df60e4c82" />

### C. Attack Settings:

<img width="358" height="404" alt="Screenshot 2025-11-12 134209" src="https://github.com/user-attachments/assets/a2d66014-f7f9-40f3-b219-56cced678b7c" />




