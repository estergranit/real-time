# RealTimeGame

A real-time multiplayer game server built using .NET 8 WebSockets.  
The server listens for incoming WebSocket connections and routes JSON messages to feature-specific handlers.

---

## ✅ Implemented Features

This version is focused on core real-time communication:

- WebSocket server established and listening on `ws://localhost:5000/`
- JSON message routing system
- Echo handler to validate routing flow end-to-end
- ✅ Serilog structured logging integrated into WebSocket flow  
  (replaces Console.WriteLine with structured logs)

---

## 🧩 Architecture Overview

Current structure:

```
RealTimeGame
 ├─ Server   → WebSocket endpoint + routing system
 ├─ Domain   → Game logic & player state (planned)
 └─ Client   → Console WebSocket client for testing (planned)
```

🎯 The routing system supports clean future expansion

---

## 🔌 WebSocket Usage Example

Send via WebSocket client:

```json
{
  "type": "Echo",
  "data": {
    "text": "Hello World!"
  }
}
```

Expected response from server:
```
Echo-Routed: Hello World!
```

---

## 🛠️ Run Locally

Start server:

```bash
dotnet run --project RealTimeGame.Server
```

Test with any WebSocket tool such as:
- PieSocket WebSocket Tester  
- WebSocket King Client  

Connect to:
```
ws://localhost:5000/
```

---

## 🚀 Next Steps

The upcoming features in priority order:

- [ ] Implement Login handler (DeviceId → PlayerId storage)
- [ ] Update client console to support login request