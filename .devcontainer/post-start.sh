#!/bin/bash

# Post-start script for GitHub Codespaces
echo "🔄 Codespace started - checking environment..."

# Ensure permissions are correct
chmod +x .devcontainer/*.sh

# Check if dependencies are installed
echo "✅ Checking .NET..."
dotnet --version

echo "✅ Checking Node.js..."
node --version

echo "✅ Checking Angular CLI..."
ng version --skip-confirmation || echo "⚠️  Angular CLI not available"

echo "✅ Environment ready for development!"
echo ""
echo "💡 Tip: Use 'Ctrl+Shift+P' -> 'Tasks: Run Task' to start backend and frontend services"