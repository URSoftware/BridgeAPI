# 🚀 Render.com & UptimeRobot - Configuration Verification

**Status**: Setup Checklist for User Verification  
**Date**: Current Session  

---

## ✅ RENDER.COM CONFIGURATION CHECKLIST

### Step 1: Verify Web Service Status

In **Render Dashboard** (https://dashboard.render.com):

1. **Check Overview Tab**
   - [ ] Service name appears: `bridgeapi` (or similar)
   - [ ] Status shows: **"Live"** (green indicator)
   - [ ] Region: `Ohio` or your selected region
   - [ ] Runtime: `.NET`

2. **Check Deployment Status**
   - [ ] Current deployment: **"Success"** (green)
   - [ ] Last deploy time: Recent (within last hour or as expected)
   - [ ] No red error banners visible

3. **Verify API URL**
   - [ ] Copy your API URL from dashboard
   - [ ] Format: `https://bridgeapi-xxxxx.onrender.com`
   - [ ] Paste here: `https://bridgeapi-____________.onrender.com`

### Step 2: Test API Endpoint

1. **Health Check Test**
   ```bash
   # Copy and paste URL from above with /api/health
   curl https://bridgeapi-xxxxx.onrender.com/api/health
   
   # Should return 200 OK with:
   # {"status":"healthy","timestamp":"...","version":"1.0.0"}
   ```
   - [ ] Response: **200 OK**
   - [ ] Status: `"healthy"`

2. **Swagger UI Test**
   ```
   https://bridgeapi-xxxxx.onrender.com/swagger/index.html
   ```
   - [ ] Page loads without errors
   - [ ] Shows API documentation
   - [ ] All endpoints listed

### Step 3: Environment Variables Check

In **Render Dashboard** → Select Service → **Settings**:

1. **Environment Variables Section**
   - [ ] `ASPNETCORE_URLS` = `http://0.0.0.0:10000`
   - [ ] `ASPNETCORE_ENVIRONMENT` = `Production`

2. **Build Settings**
   - [ ] Build Command: `dotnet build --configuration Release`
   - [ ] Start Command: `dotnet BridgeAPI.dll`

### Step 4: GitHub Integration

In **Render Dashboard** → Select Service → **Settings**:

1. **Deployment Trigger**
   - [ ] Repository: `URSoftware/BridgeAPI`
   - [ ] Branch: `main` (or `beta` if testing)
   - [ ] Auto-deploy enabled: ✅ Yes

**Note**: Future pushes to `main` will auto-deploy!

---

## ✅ UPTIMEROBOT CONFIGURATION CHECKLIST

### Step 1: Verify Monitor Creation

In **UptimeRobot Dashboard** (https://uptimerobot.com/dashboard):

1. **Check Monitor List**
   - [ ] Monitor name appears: `BridgeAPI Keep-Alive` (or similar)
   - [ ] Type: **HTTP(s)**
   - [ ] Status indicator: **Up** (green) or **Waiting**

2. **Monitor Details**
   ```
   Name: BridgeAPI Keep-Alive
   URL: https://bridgeapi-xxxxx.onrender.com/api/health
   Method: GET
   Interval: 5 minutes
   Timeout: 30 seconds
   ```
   - [ ] URL is correct
   - [ ] Interval: **5 minutes** (prevents sleep)
   - [ ] Status: Green indicator

### Step 2: Check Monitor Response

1. **Last Check Status**
   - [ ] Shows: **Up** (green checkmark)
   - [ ] Last checked: Within last 5 minutes
   - [ ] Response time: < 1000ms (usually <500ms)

2. **Uptime Percentage**
   - [ ] Shows: ~100% (unless just created)
   - [ ] Uptime badge available for sharing

### Step 3: Alert Configuration (Optional)

In **UptimeRobot** → Monitor Details → **Alert Contacts**:

1. **Email Alerts**
   - [ ] Email address configured: (your email)
   - [ ] Alert when down: **Yes**
   - [ ] Threshold: 2 consecutive failures (recommended)

2. **Slack Integration** (Optional)
   - [ ] Slack workspace connected (if desired)
   - [ ] Alerts sent to #channel or @user

---

## ✅ INTEGRATION TEST

### Test 1: Verify API Responds

```bash
# Render production URL
curl -I https://bridgeapi-xxxxx.onrender.com/api/health

# Expected output:
# HTTP/1.1 200 OK
# Content-Type: application/json
```

**Status**: [ ] ✅ API responds | [ ] ❌ No response

### Test 2: Verify UptimeRobot is Monitoring

1. Go to **UptimeRobot Dashboard**
2. Check your monitor
3. Look for **"Last check"** timestamp
4. Should show: **"a few seconds/minutes ago"** ✅

**Status**: [ ] ✅ UptimeRobot monitoring | [ ] ❌ No checks yet

### Test 3: Verify Auto-Deployment

1. Make a small change to a file in GitHub
2. Wait 1-2 minutes
3. Go to **Render Dashboard**
4. Check **Deployments** tab
5. Should show new deployment in progress

**Status**: [ ] ✅ Auto-deploy working | [ ] ❌ No new deployment

---

## 📊 COMPLETE CONFIGURATION SUMMARY

| Service | Component | Status | Details |
|---------|-----------|--------|---------|
| **Render** | Web Service | [ ] Done | `bridgeapi-xxxxx.onrender.com` |
| **Render** | Environment Vars | [ ] Done | ASPNETCORE_URLS + Environment |
| **Render** | GitHub Integration | [ ] Done | Auto-deploy from `main` |
| **Render** | Health Endpoint | [ ] Done | /api/health → 200 OK |
| **UptimeRobot** | Monitor | [ ] Done | `BridgeAPI Keep-Alive` |
| **UptimeRobot** | Monitoring Working | [ ] Done | 5-min pings active |
| **UptimeRobot** | Alerts | [ ] Optional | Email/Slack configured |

---

## 🎯 SUCCESS CRITERIA

When you complete this checklist, verify:

✅ [ ] Render dashboard shows service **"Live"**  
✅ [ ] API health check returns **200 OK**  
✅ [ ] UptimeRobot monitor shows **"Up"** (green)  
✅ [ ] Last check in UptimeRobot shows **recent** (< 5 min ago)  
✅ [ ] GitHub auto-deploy configured for `main` branch  
✅ [ ] Can access Swagger UI at `/swagger/index.html`  

---

## 🔧 TROUBLESHOOTING

### If Render shows "Build Failed"
1. Check **Logs** tab in Render
2. Look for `.NET` or `dotnet` errors
3. Verify environment variables are set
4. Try manual redeploy: **"Re-run latest deployment"**

### If UptimeRobot shows "Down"
1. Verify Render API is **"Live"** status
2. Check URL is exactly correct (copy-paste from Render)
3. Test manually: `curl https://bridgeapi-xxxxx.onrender.com/api/health`
4. Wait 1-2 minutes for next UptimeRobot check
5. Refresh UptimeRobot dashboard

### If Auto-Deploy Not Working
1. Go to Render → Settings
2. Check **Repository**: `URSoftware/BridgeAPI` ✅
3. Check **Branch**: `main` ✅
4. Check **GitHub authenticated**: ✅
5. Re-authorize GitHub connection if needed

---

## 📸 Screenshots to Take (For Reference)

1. **Render Dashboard** showing service **"Live"**
2. **UptimeRobot Dashboard** showing monitor **"Up"**
3. **API Health Response** from browser: `/api/health`
4. **Swagger UI** at `/swagger/index.html`

---

## 🎉 Once Everything is Configured

Your BridgeAPI will be:

✅ **Online 24/7** - Never sleeps  
✅ **Monitored** - UptimeRobot pings every 5 minutes  
✅ **Auto-deploying** - New code on `main` = auto-deploy  
✅ **Production-ready** - Professional hosted API  
✅ **Always accessible** - Via `https://bridgeapi-xxxxx.onrender.com`  

---

## 📞 Quick Links for Reference

| Service | URL |
|---------|-----|
| **Render Dashboard** | https://dashboard.render.com |
| **UptimeRobot Dashboard** | https://uptimerobot.com/dashboard |
| **GitHub Repository** | https://github.com/URSoftware/BridgeAPI |
| **Your API** | https://bridgeapi-xxxxx.onrender.com |
| **API Docs** | https://bridgeapi-xxxxx.onrender.com/swagger/index.html |

---

**Created**: Current Session  
**Status**: Ready for Your Configuration Review  
**Next**: Verify each item above and mark as complete
