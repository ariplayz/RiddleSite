Deploying RiddleSite to Linux with Kestrel

This repo is already configured so the RiddleSite.Browser project can act as both:
- The WebAssembly client (net9.0-browser) and
- A Kestrel static-file host (net9.0) that serves the published WASM from wwwroot.

Follow the steps below to publish and run on a Linux server.

Prerequisites on the Linux server
- .NET 9 Runtime (for framework-dependent) or none (for self-contained).
- A non-root user for running the app.
- Optionally Nginx to act as a reverse proxy and TLS terminator.

Install .NET 9 on Ubuntu (example)
1) Add Microsoft package feed and install the runtime:
   - sudo apt-get update
   - sudo apt-get install -y wget apt-transport-https
   - wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   - sudo dpkg -i packages-microsoft-prod.deb
   - sudo apt-get update
   - sudo apt-get install -y dotnet-runtime-9.0
   - Optional (if you plan to publish and build on server): sudo apt-get install -y dotnet-sdk-9.0

Publish on your dev machine
From the repository root on Windows/Dev machine:

Framework-dependent (requires dotnet runtime installed on server):
- dotnet publish .\RiddleSite.Browser\RiddleSite.Browser.csproj -c Release -f net9.0 -o .\publish

Self-contained (no runtime required on server):
- linux-x64:
  dotnet publish .\RiddleSite.Browser\RiddleSite.Browser.csproj -c Release -f net9.0 -r linux-x64 --self-contained true -p:PublishReadyToRun=true -o .\publish-linux-x64
- arm64 (Raspberry Pi 4 / ARM servers):
  dotnet publish .\RiddleSite.Browser\RiddleSite.Browser.csproj -c Release -f net9.0 -r linux-arm64 --self-contained true -p:PublishReadyToRun=true -o .\publish-linux-arm64

Notes
- The publish step automatically builds the browser target (net9.0-browser) and places those assets under publish/wwwroot.
- appsettings.Production.json binds Kestrel to http://0.0.0.0:8080 by default. You can override with ASPNETCORE_URLS.

Upload the publish folder to the server
- Create a directory on the server, e.g.: /var/www/riddlesite
- Copy the contents of your publish folder to that directory (via scp, sftp, rsync, etc.). For example:
  scp -r .\publish/* user@server:/var/www/riddlesite/

Run the app with Kestrel (manual)
Framework-dependent:
- cd /var/www/riddlesite
- ASPNETCORE_ENVIRONMENT=Production dotnet RiddleSite.Browser.dll

Self-contained:
- cd /var/www/riddlesite
- export ASPNETCORE_ENVIRONMENT=Production
- For linux-x64: chmod +x RiddleSite.Browser && ./RiddleSite.Browser

Custom ports/URLs
- You can override URLs via environment variable or command line:
  - ASPNETCORE_URLS=http://0.0.0.0:8080
  - Or: dotnet RiddleSite.Browser.dll --urls "http://0.0.0.0:8080"

Test
- From your workstation: http://your-server-ip:8080/
- Health endpoint: http://your-server-ip:8080/health

Run as a systemd service (recommended)
1) Copy the provided sample unit to the server and edit paths if needed:
   - File: deploy/riddlesite.service
   - sudo cp deploy/riddlesite.service /etc/systemd/system/riddlesite.service
2) Reload and start:
   - sudo systemctl daemon-reload
   - sudo systemctl enable riddlesite
   - sudo systemctl start riddlesite
3) Check status/logs:
   - systemctl status riddlesite
   - journalctl -u riddlesite -f

Sample Nginx reverse proxy (optional)
If you want to expose on ports 80/443 with TLS via Nginx:

server {
    listen 80;
    server_name your-domain.com;

    location / {
        proxy_pass         http://127.0.0.1:8080;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}

Harden for production
- Keep Kestrel behind Nginx or another reverse proxy for internet-facing production.
- Restrict firewall to expose only 80/443 (or your reverse proxy ports).
- Use a real TLS cert (e.g., Let’s Encrypt) on the reverse proxy.
- Adjust Kestrel limits in appsettings.Production.json as needed.

Troubleshooting
- If port 8080 is in use, change it in appsettings.Production.json or via ASPNETCORE_URLS.
- Ensure the working directory contains wwwroot and the DLL/binary.
- On SELinux-enabled distros, ensure appropriate context for /var/www/riddlesite.
- Use journalctl -u riddlesite -f to see live logs when running as a service.
