# GitHub Codespaces Configuration

This project is fully configured for GitHub Codespaces, providing a complete development environment in the cloud.

## Quick Start with Codespaces

1. **Open in Codespaces**
   - Navigate to the GitHub repository
   - Click the green "Code" button
   - Select "Codespaces" tab
   - Click "Create codespace on main"

2. **Wait for Setup**
   - Codespaces will automatically configure the environment
   - This includes installing .NET 8, Node.js, and all dependencies

3. **Start Development**
   - Backend and frontend will be automatically configured
   - Use the integrated terminal to run commands

## Automatic Setup

When you open the Codespace, the following happens automatically:

### Environment Setup
- ✅ .NET 8 SDK installed
- ✅ Node.js 20 installed  
- ✅ Angular CLI installed globally
- ✅ All backend dependencies restored
- ✅ All frontend dependencies installed

### VS Code Extensions
- ✅ C# Dev Kit
- ✅ Angular Language Service
- ✅ TypeScript support
- ✅ Docker support
- ✅ GitHub tools

## Running the Application

### Option 1: Using VS Code Tasks (Recommended)

1. Open Command Palette (`Ctrl+Shift+P` or `Cmd+Shift+P`)
2. Type "Tasks: Run Task"
3. Select one of the predefined tasks:
   - **"Start Backend"** - Runs the Azure Functions backend
   - **"Start Frontend"** - Runs the Angular development server
   - **"Start Both"** - Runs both backend and frontend simultaneously

### Option 2: Manual Terminal Commands

Open two terminals in VS Code:

**Terminal 1 - Backend:**
```bash
cd backend
dotnet restore
dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd frontend
npm install
npm start
```

## Port Forwarding

Codespaces automatically forwards the following ports:
- **7071** - Backend API (Azure Functions)
- **4200** - Frontend (Angular dev server)

These ports will be accessible via generated URLs that Codespaces provides.

## Development Workflow

### 1. Making Changes

Edit files using the VS Code interface. The integrated editor provides:
- IntelliSense for C# and TypeScript
- Debugging support
- Git integration
- Extensions for enhanced productivity

### 2. Testing Changes

**Backend Testing:**
```bash
cd backend
dotnet test
```

**Frontend Testing:**
```bash
cd frontend
npm test
```

**Building:**
```bash
# Backend
cd backend
dotnet build

# Frontend  
cd frontend
npm run build
```

### 3. Debugging

#### Backend Debugging
1. Open a .cs file in the backend
2. Set breakpoints by clicking in the gutter
3. Press F5 or use the Run and Debug panel
4. Select "Attach to .NET Functions"

#### Frontend Debugging
1. Start the frontend with `npm start`
2. Open browser dev tools in the forwarded port URL
3. Use VS Code's built-in browser debugging
4. Set breakpoints in .ts files

## Environment Variables

Codespaces automatically sets up development environment variables:

### Backend Environment
- `ASPNETCORE_ENVIRONMENT=Development`
- `AzureWebJobsScriptRoot` (automatically configured)

### Frontend Environment
- `NODE_ENV=development`
- API base URL automatically configured for Codespaces

## Database Configuration

In Codespaces, the backend uses:
- **In-Memory Database** for development
- Entity Framework Core with seed data
- No external database setup required

## File Synchronization

Changes made in Codespaces are automatically synced with the GitHub repository:
- Edit files in the web interface
- Use Git integration for commits
- Push changes directly to your branch

## Performance Tips

### For Better Performance:
1. **Use the web interface** for most editing tasks
2. **Keep terminal sessions minimal** - close unused terminals
3. **Use tasks** instead of running multiple commands manually
4. **Commit frequently** to avoid losing work

### Resource Management:
- Codespaces automatically manages resources
- The environment will hibernate after inactivity
- Data persists across hibernation cycles

## Networking and APIs

### API Communication
The frontend automatically connects to the backend through:
- Forwarded ports (7071 → generated URL)
- CORS configured for Codespaces domains
- Automatic URL resolution

### External APIs
If your application needs external APIs:
- They work normally from Codespaces
- No special configuration required
- Same security considerations apply

## Container Features

This Codespace includes:
- **Docker** for container development
- **Azure CLI** for Azure resources
- **GitHub CLI** for GitHub integration
- **Common development tools**

## Troubleshooting

### Common Issues:

**Issue**: Backend doesn't start
```bash
# Solution: Restore dependencies
cd backend
dotnet clean
dotnet restore
dotnet run
```

**Issue**: Frontend compilation errors
```bash
# Solution: Clear cache and reinstall
cd frontend
rm -rf node_modules package-lock.json
npm install
```

**Issue**: Port not accessible
- Check the "Ports" tab in VS Code
- Ensure the service is running
- Try stopping and restarting the service

**Issue**: Out of storage space
- Codespaces provides 32GB of storage
- Clean up unused files: `docker system prune`
- Remove `node_modules` if needed: `find . -name "node_modules" -type d -exec rm -rf {} +`

## Advanced Features

### Dev Containers
This project uses dev containers for consistent environments:
- Configuration in `.devcontainer/devcontainer.json`
- Automatic extension installation
- Custom shell configuration

### Debugging Configuration
Pre-configured debugging for:
- .NET backend with breakpoints
- Angular frontend debugging
- Integrated terminal debugging

### Git Integration
Full Git functionality including:
- Visual diff editor
- Commit/push directly from interface
- Branch management
- Pull request creation

## Extending the Environment

### Adding VS Code Extensions
1. Open Extensions panel (`Ctrl+Shift+X`)
2. Search and install extensions
3. Extensions are automatically saved to your Codespace

### Adding System Packages
Edit `.devcontainer/devcontainer.json` to add packages:
```json
{
  "features": {
    "ghcr.io/devcontainers/features/common-utils:2": {
      "packages": "package1,package2"
    }
  }
}
```

### Custom Scripts
Add scripts to `.devcontainer/` for custom setup:
- `postCreateCommand` - Runs after container creation
- `postStartCommand` - Runs after container starts
- `postAttachCommand` - Runs after attaching to container

## Collaboration

### Live Share Integration
- Share your Codespace with team members
- Real-time collaborative editing
- Shared terminal sessions
- Shared debugging sessions

### Code Reviews
- Create pull requests directly from Codespaces
- Review code with full IDE features
- Test changes in isolated environment

## Best Practices

### Development Workflow
1. **Create feature branches** for new work
2. **Use descriptive commit messages**
3. **Test thoroughly** before pushing
4. **Document changes** in pull requests

### Resource Management
1. **Stop Codespace** when not in use
2. **Delete unused Codespaces** to save quota
3. **Monitor storage usage** regularly
4. **Use .gitignore** to exclude large files

### Security
1. **Never commit secrets** to the repository
2. **Use environment variables** for configuration
3. **Be cautious with external packages**
4. **Review dependencies** regularly

## Getting Help

If you encounter issues with Codespaces:

1. **Check the VS Code output panel** for errors
2. **Review the terminal output** for clues
3. **Restart the Codespace** if needed
4. **Create an issue** with detailed information

## Comparison: Local vs Codespaces

| Feature | Local Development | GitHub Codespaces |
|---------|------------------|-------------------|
| Setup Time | 30-60 minutes | 2-3 minutes |
| Environment Consistency | Varies by developer | Always consistent |
| Resource Requirements | Your machine | Cloud resources |
| Collaboration | Complex setup | Built-in |
| Access | Requires local machine | Any device with browser |
| Cost | Hardware/electricity | GitHub quota |

## Codespaces Limits

Be aware of these limits:
- **Monthly quota** based on GitHub plan
- **Maximum concurrent Codespaces** (varies by plan)
- **Storage limit** (32GB per Codespace)
- **Compute hours** counted against quota

---

**Happy coding in the cloud! 🚀**