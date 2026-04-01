# Render.com & UptimeRobot Configuration Checklist

## ✅ Pre-Deployment Verification

- [x] API compiled successfully in Release mode
- [x] API responding to health check: `http://localhost:5002/api/health` → 200 OK
- [x] Git repository at: https://github.com/URSoftware/BridgeAPI
- [x] All documentation centralized in `/docs` folder
- [x] Professional README.md at root
- [x] Git configured with profile: `dev@ursoftware.com / URSoftware Development`
- [x] Latest commit: Documentation reorganization

---

## 📋 Render.com Deployment Checklist

### Account & Repository Setup
- [ ] Sign up at https://render.com (free account)
- [ ] Ensure GitHub authentication is connected
- [ ] Verify access to https://github.com/URSoftware/BridgeAPI

### Web Service Configuration
- [ ] Create new Web Service in Render Dashboard
- [ ] Connect GitHub repository: `URSoftware/BridgeAPI`
- [ ] Set service name: `bridgeapi`
- [ ] Set region: `Ohio` (or preferred)
- [ ] Set runtime: `.NET`
- [ ] Set build command: `dotnet build --configuration Release`
- [ ] Set start command: `dotnet BridgeAPI.dll`

### Environment Variables (in Render)
```
ASPNETCORE_URLS = http://0.0.0.0:10000
ASPNETCORE_ENVIRONMENT = Production
```

### Deployment
- [ ] Click "Create Web Service"
- [ ] Wait for deployment to complete (Status should show "Live")
- [ ] Note the deployed URL: `https://bridgeapi-xxxx.onrender.com`
- [ ] Test endpoint: `https://bridgeapi-xxxx.onrender.com/api/health`

---

## 🔔 UptimeRobot Configuration Checklist

### Account Setup
- [ ] Sign up at https://uptimerobot.com (free account)
- [ ] Verify email address

### Monitor Configuration
- [ ] Log in to UptimeRobot Dashboard
- [ ] Click "Add Monitor" or "Add Monitoring"
- [ ] Select Monitor Type: **"HTTP(s)"**

### Monitor Settings
| Setting | Value |
|---------|-------|
| Friendly Name | `BridgeAPI Keep-Alive` |
| URL to monitor | `https://bridgeapi-xxxx.onrender.com/api/health` |
| Monitor interval | `5 minutes` |
| HTTP method | `GET` |
| Timeout | `30 seconds` |
| Alert after | `2 failures` |

### Activation
- [ ] Click "Create Monitor"
- [ ] Verify status changes to **"Up"** within 1-2 minutes
- [ ] (Optional) Add email alerts in settings

---

## 🧪 Testing & Verification

### Local Testing
```bash
# In Terminal, after API is running on 5002
Invoke-WebRequest -Uri "http://localhost:5002/api/health" -UseBasicParsing
# Expected: StatusCode 200
```

### Production Testing (after Render deployment)
```bash
# From any terminal/tool
curl -i https://bridgeapi-xxxx.onrender.com/api/health
# Expected: HTTP/1.1 200 OK
```

### Monitoring Verification
- [ ] Render dashboard shows service as "Live"
- [ ] Render shows "Build successful"
- [ ] UptimeRobot shows monitor as "Up"
- [ ] UptimeRobot displays uptime percentage (should be ~100%)
- [ ] Last check timestamp is recent (within a few minutes)

---

## 📊 Final Configuration Summary

| Component | Status | URL |
|-----------|--------|-----|
| **API (Local)** | ✅ Running | `http://localhost:5002` |
| **Repository** | ✅ Ready | https://github.com/URSoftware/BridgeAPI |
| **Render Service** | ⏳ Pending | `https://bridgeapi-xxxx.onrender.com` |
| **UptimeRobot Monitor** | ⏳ Pending | Monitor dashboard |
| **Health Endpoint** | ✅ Working | `/api/health` |

---

## 🎯 Success Criteria

All items must be checked:
- [ ] Render deployment shows "Live" status
- [ ] API responds with 200 OK to health check
- [ ] UptimeRobot monitor shows "Up" status
- [ ] UptimeRobot logs successful pings (5-min interval)
- [ ] No errors in Render build logs
- [ ] API running for >5 minutes without restart

When all items are checked, your BridgeAPI is **production-ready and always-on**! 🚀

---

## 🔗 Quick Links

- **Render Dashboard**: https://dashboard.render.com
- **Render Docs**: https://docs.render.com/deploy-from-git
- **UptimeRobot Dashboard**: https://uptimerobot.com/dashboard
- **GitHub Repository**: https://github.com/URSoftware/BridgeAPI
- **API Swagger UI** (when deployed): `https://bridgeapi-xxxx.onrender.com/swagger/index.html`

---

**Documentation Version**: 1.0  
**Last Updated**: Current Session  
**Status**: Ready for Configuration
