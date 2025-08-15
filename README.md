# RTS Builder Prototype

A Unity prototype demonstrating core RTS mechanics including building placement, resource management, and AI workers — built with SOLID principles and optimized for performance.

## 🎮 Features
- Smooth RTS camera with edge scrolling & zoom
- Grid & free building placement with collision checks
- Resource system (wood, stone, gold)
- AI workers with state machine logic
- Object pooling for performance
- Modular architecture for easy expansion

## 🛠 Tech Stack
- Unity 2023 LTS
- C# (SOLID principles)
- Unity Profiler for performance tuning

## 📂 Repository Structure
See `ARCHITECTURE.md` for detailed system layout.

## 🚀 How to Run
1. Clone the repository
2. Open in Unity 2023 LTS
3. Load the `MainScene` in `Assets/_Project/Scenes`
4. Play!

## 📸 Screenshots
![Building Placement](Docs/building_placement.gif)

## 📊 Performance
Object pooling reduced GC Alloc spikes by 65% (Profiler screenshot in `Docs`).

---

## 📜 License
MIT
