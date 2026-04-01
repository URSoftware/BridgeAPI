# 🚀 BridgeAPI - Final Deployment Guide

**Status**: API is running locally on `http://localhost:5002` and ready for production deployment.

---

## Step 1: Deploy to Render.com (5 minutes)

### Prerequisites
- GitHub account with repository access: https://github.com/URSoftware/BridgeAPI
- Free Render account: https://render.com

### Deployment Steps

1. **Go to Render Dashboard**: https://dashboard.render.com
2. **Click "New +"** → Select **"Web Service"**
3. **Connect GitHub Repository**:
   - Click "Connect a repository"
   - Search for `BridgeAPI`
   - Select `URSoftware/BridgeAPI`
   - Click "Connect"

4. **Configure Web Service**:
   | Setting | Value |
   |---------|-------|
   | Name | `bridgeapi` |
   | Environment | `.NET` |
   | Region | `Ohio` (or closest to you) |
   | Branch | `master` |
   | Runtime | `.NET` |
   | Build Command | `dotnet build --configuration Release` |
   | Start Command | `dotnet BridgeAPI.dll` |

5. **Advanced Settings**:
   - Scroll down and click **"Advanced"**
   - Add Environment Variable:
     ```
     ASPNETCORE_URLS = http://0.0.0.0:10000
     ASPNETCORE_ENVIRONMENT = Production
     ```

6. **Create Web Service**:
   - Click **"Create Web Service"**
   - Wait for deployment (2-3 minutes)
   - You'll see: `⠙ Building...` → `✓ Live!`

7. **Get Your URL**:
   - After deployment completes, you'll see the live URL: `https://bridgeapi-xxxx.onrender.com`

### Verify Deployment
```bash
curl https://bridgeapi-xxxx.onrender.com/api/health
# Expected response: {"status":"ok"} with 200 OK
```

---

## Step 2: Set Up UptimeRobot (5 minutes)

### Why UptimeRobot?
- **Prevents sleeping**: Render puts free apps to sleep after 15 min of inactivity
- **Always active**: Pings your API every 5 minutes
- **Free forever**: No cost, includes email alerts

### Setup Steps

1. **Create UptimeRobot Account**: https://uptimerobot.com/signup

2. **Create Monitor**:
   - Log in to https://uptimerobot.com/
   - Click **"Add Monitoring"** or **"+ Add Monitor"**
   - Select Monitor Type: **"HTTP(s)"**

3. **Configure Monitor**:
   | Setting | Value |
   |---------|-------|
   | Friendly Name | `BridgeAPI Health` |
   | URL | `https://bridgeapi-xxxx.onrender.com/api/health` |
   | Monitoring Interval | `5 minutes` |
   | HTTP Method | `GET` |

4. **Save Monitor**:
   - Click **"Create Monitor"**
   - You'll see status: **"Up"** (or shortly after)

### Optional: Add Alerts
1. Go to **"My Settings"** → **"Alert Contacts"**
2. Add your email or Slack
3. Click **"Add"**
4. Back to monitor, click **"Edit"**
5. Under "Alert Contacts", select your contact
6. Enable "Alert if down X times"
7. **Save**

---

## Step 3: Verify Everything Is Working

### Test Health Endpoint
```bash
# For local testing
curl http://localhost:5002/api/health

# For production (after Render deployment)
curl https://bridgeapi-xxxx.onrender.com/api/health
```

### Expected Response
```json
{
  "status": "ok"
}
```

### Test Database Connection (Example)
```bash
curl -X POST https://bridgeapi-xxxx.onrender.com/api/database/connect \
  -H "Content-Type: application/json" \
  -d '{
    "server": "your-server.database.windows.net",
    "database": "YourDB",
    "userId": "admin",
    "password": "YourPassword",
    "databaseType": "SqlServer"
  }'
```

---

## Documentation Links

| Document | Purpose |
|----------|---------|
| [SETUP.md](./SETUP.md) | Local development setup |
| [DEPLOYMENT.md](./DEPLOYMENT.md) | All deployment platforms |
| [RENDER_KEEPALIVE.md](./RENDER_KEEPALIVE.md) | Keep-alive solutions |
| [QUICK_DEPLOY.md](./QUICK_DEPLOY.md) | Quick reference |

---

## Troubleshooting

### API showing "Deployment in progress"
- Wait 2-3 minutes for first deployment
- Check build logs in Render dashboard

### UptimeRobot shows "Down"
1. Verify Render deployment is "Live"
2. Check URL is correct: `https://bridgeapi-xxxx.onrender.com/api/health`
3. Ensure `/api/health` endpoint is working

### Port already in use error (Local)
```bash
# Change port
$env:ASPNETCORE_URLS="http://localhost:5002"; dotnet run
```

---

## Summary

✅ **Local API**: Running on `http://localhost:5002`
✅ **Production API**: Deploy to Render.com (5 min setup)
✅ **Keep Alive**: Configure UptimeRobot (5 min setup)
✅ **Total Setup Time**: ~10 minutes for production-ready API

### Next Commands
```bash
# Start local API on custom port
$env:ASPNETCORE_URLS="http://localhost:5002"
dotnet run --configuration Release

# Or use Docker (if installed)
docker-compose up
```

---

**Created**: 2024  
**Last Updated**: Current Session  
**Status**: Ready for Production Deployment  
