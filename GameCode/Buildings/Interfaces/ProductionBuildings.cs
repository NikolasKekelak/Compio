using ConsoleApp1.GameCode.Items;

namespace ConsoleApp1.GameCode.Building.Interfaces;


/*
 * Building description:
 * ProductionBuilding - Building which has basic Production and crafting,and energy consumption
 *  - Assembler 1 or 2 input items, 1 output item
 *    - Iron -> Rods, Plates, Screws
 *    - Copper -> Wires, Plates, Heat Exchangers, Cables
 *    - Limestone -> Concrete
 *    - 
 *  - Assembler T2, faster and more energy efficient
 *  - Factory 3 or 4 input items, 1-2 output items
 *  - Factory T2 - input can be liquid and so the output
 *  - Factory T3 - 5 or 6 input items and always 2 outputs, end game recipies
 *  - Paricle accelator - 1-3 inputs, 1-2 outputs
 *
 *  - Refinery - 1-4 input items, 1-5 output items
 *  - Chemistry plant - 2-4 input items, 1-2 output items
 *  - Chemistry plant T2 - faster and more energy efficient, extra recipies
 *  - Chemistry plant T3 - end game recipies
 *  - Boiler -boils water using coal
 *  - Electric Boiler - ceratin liquids are needed to be heated up
 *  - Cooler - Cools down liquid, mainly used for coolants
 *  - Anvil - 1 - 3 inputs, 1 output. (Turns molten metal into part more efficiently), no Liquid output
 * 
 *  -  Electric Furnace - 1-2 inputts, 1 output
 *  -  Electric Furnace T2 , more energy efficient
 *  -  Smeltery - 1-3 input, 1 liquid output, less energy efficient but more items per ore
 *  -  Industrial Furnace - 1-3 inputs, 2 liquid outputs. Power hungry, but fast and very efficient. END GAME
 *
 *  - Miner T1 - 1x mining speed. Can mine: coal, iron, copper, limestone
 *  - Miner T2 - 2x mining speed. Can mine: Silicon, Titanium, Alluminium, Platinium, Lead
 *  - Miner T3 - 4x mining speed. Can mine: Uranium, Plutonium, Gold, Diamond, Niobium
 *  - Miner T4 - 8x mining speed. END GAME
 *
 *  RESEARCH ( computers produce cartridges, while servers upload them and research them)
 *  - Computer - T1 and T2 research
 *  - Super Computer - T3 and T4 research
 *  - Quantum Computer - T5 and T6 research
 *  - Neural-Quantum Computer - T7 and T8 research
 *
 *  - Server - 1x research speed
 *
 *  Energy production
 *  - Steam Turbines T1, T2 and T3
 *  - Gas and Diesel generators
 *  - Biomass Burner (heats water)
 *  - Solar panels, Wind turbines
 *  - Nuclear Reactors (Uranium, Plutonium)
 *  - Fusion Reactors (Deuterium + Tricium = Super heated helium)
 *  
 *  - 
 */


