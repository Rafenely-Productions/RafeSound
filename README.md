# RafeSound

> A cross-platform songwriting companion for string instruments featuring an interactive fretboard, real-time reverse chord engine, and microphone tuner. Designed for playing and writing with instrument in hand.

[![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Framework](https://img.shields.io/badge/UI-MAUI-512BD4)](https://dotnet.microsoft.com/en-us/apps/maui)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Organization](https://img.shields.io/badge/Studio-Rafenely%20Studios-orange)](https://github.com/rafenely-studios)

---

## 🎯 Project Vision

When writing music on a stringed instrument (starting with ukulele and expanding to guitar, bass, or charango), your computer or phone should serve as a fluid extension of your ear—not an obstacle full of menus and delays:

1. **You play a shape that sounds good, but don't know what it is:** Tap the fret positions on the horizontal fretboard, and the app instantly names the chord, breaks down its intervals, and identifies inversions.
2. **You are stuck wondering where to go next:** The harmonic assistant analyzes your current context and suggests functional next chords (tonic, subdominant, dominant, common cadences).
3. **You want to preserve the idea immediately:** Push chords directly into structured song sections (`Verse`, `Chorus`, `Bridge`) alongside your lyric lines.
4. **Before jumping in:** Tune up quickly by ear or via microphone pitch detection without switching apps.

---

## ✨ Key Features

* **Interactive Horizontal Fretboard:**
  * Horizontal layout matching the player's perspective looking down at the neck.
  * Per-string touch controls supporting fretted notes (1–12), open strings (0), and muted strings (X).
* **Reverse Chord Engine:**
  * Deterministic C# chord detection algorithm (major, minor, augmented, diminished triads; 7th chords, suspensions, extended voicings, and slash chords/inversions).
* **Harmonic Progression Assistant ("What sounds good next?"):**
  * Context-aware recommendations driven by diatonic scale degrees and tension/resolution principles.
* **Songwriting Scratchpad:**
  * Section-based song architecture (`Intro`, `Verse`, `Pre-Chorus`, `Chorus`, `Bridge`, `Outro`).
  * Lyric attachment alongside chord progressions in a fast, editable flow.
  * Instant local persistence.
* **Built-in Microphone Pitch Tuner:**
  * Low-latency pitch detection optimized for acoustic string frequencies with real-time visual feedback.

---

## 🏗️ System Architecture

RafeSound is built following **Clean Architecture (Ports & Adapters)** principles. The core music theory and acoustic mechanics are written in pure C# with zero UI or operating system dependencies, making them fully portable and instantly testable.

```text
┌─────────────────────────────────────────────────────────────┐
│                    UI Layer (.NET MAUI)                     │
│  - Horizontal Fretboard Control       - Song Scratchpad     │
│  - Reactive Tuner Gauge               - ViewModels / State  │
└──────────────────────────────┬──────────────────────────────┘
                               │
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                   Application Layer (.NET)                   │
│  - Use Cases: IdentifyChord, SuggestNextChords              │
│  - Song Management: AddSection, AttachLyrics                │
│  - Port Contracts: ISongRepository, IPitchDetector          │
└──────────────┬───────────────────────────────┬──────────────┘
               │                               │
               ▼                               ▼
┌──────────────────────────────┐ ┌────────────────────────────┐
│      Domain / Core (.NET)    │ │    Infrastructure Layer    │
│  - Pitch, Note, Interval     │ │  - SQLite / Local Storage  │
│  - Fretboard, Tuning, String │ │  - Audio Capture / DSP     │
│  - ChordDetector, Harmony    │ │    (Platform Mic Services) │
└──────────────────────────────┘ └────────────────────────────┘

Solution Breakdown
| Project | Type | Responsibility |
|---|---|---|
| RafeSound.Core | Class Library | Acoustic math, pitch classes, intervals, tunings, fretboard descriptors, and reverse chord detection algorithm. Zero external dependencies. |
| RafeSound.Application | Class Library | Songwriting domain models (Song, Section, LyricLine), use-case orchestrators, and port definitions. |
| RafeSound.Infrastructure.Data | Class Library | SQLite persistence and repository implementations. |
| RafeSound.Infrastructure.Audio | Class Library | Digital signal processing (DSP) and pitch estimation algorithms (Autocorrelation / YIN). |
| RafeSound.Maui | Multi-target App | Cross-platform UI, native microphone buffer bridge, and fretboard canvas rendering. |
| RafeSound.Core.Tests | xUnit Test Project | Unit test suite validating chord logic, interval math, and harmonic progressions. |
🎸 Instrument Abstraction
While initial delivery targets Standard Ukulele (G-C-E-A), instruments are decoupled through descriptors:
// Default target: Standard Ukulele
public static readonly InstrumentDescriptor UkuleleStandard = new(
    Name: "Ukulele",
    StringCount: 4,
    Frets: 12,
    Tuning: Tuning.StandardGCEA
);

// Easily extensible without touching the core engine:
public static readonly InstrumentDescriptor GuitarStandard = new(
    Name: "Guitar",
    StringCount: 6,
    Frets: 21,
    Tuning: Tuning.StandardEADGBE
);

🚀 Getting Started
Prerequisites
 * .NET SDK (latest LTS or current SDK).
 * .NET MAUI workload installed:
   dotnet workload install maui

Clone & Build
git clone [https://github.com/rafenely-studios/rafesound.git](https://github.com/rafenely-studios/rafesound.git)
cd rafesound
dotnet restore
dotnet build

Run Tests
Test the core theory engine in milliseconds without spinning up mobile emulators:
dotnet test tests/RafeSound.Core.Tests

🗺️ Roadmap
Phase 1: Core Engine & Fretboard (MVP)
 * [ ] Music primitives modeling (Note, Interval, PitchClass, Tuning).
 * [ ] Deterministic reverse chord identifier algorithm in RafeSound.Core.
 * [ ] Comprehensive unit test suite covering triads, 7ths, suspensions, and inversions.
 * [ ] Interactive horizontal fretboard UI component.
 * [ ] Real-time chord identification display card.
Phase 2: Songwriting Scratchpad & Harmony
 * [ ] Songwriting persistence models (Song, Section, Lyrics).
 * [ ] Diatonic recommendation engine ("What chord fits next?").
 * [ ] Section editor supporting verses, choruses, and inline chord markers.
 * [ ] Local storage via SQLite.
Phase 3: Microphone Tuner
 * [ ] Audio pitch detection DSP module (autocorrelation).
 * [ ] Multi-platform microphone buffer bridge.
 * [ ] Real-time visual tuning gauge.
Phase 4: Expansion
 * [ ] Instrument descriptors for standard acoustic guitar and bass.
 * [ ] Export to ChordPro and plain text format.
 * [ ] Plucked string audio playback feedback.
📄 License
Distributed under the MIT License. See LICENSE for details.
Crafted with passion by Rafenely Studios.
