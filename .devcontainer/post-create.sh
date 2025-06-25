#!/bin/bash

# Post-create script for GitHub Codespaces
echo "🚀 Setting up Event Management System development environment..."

# Install Angular CLI globally
echo "📦 Installing Angular CLI..."
npm install -g @angular/cli

# Install Azure Functions Core Tools (if available)
echo "📦 Attempting to install Azure Functions Core Tools..."
npm install -g azure-functions-core-tools@4 --unsafe-perm true || echo "⚠️  Azure Functions Core Tools installation skipped (network restrictions)"

# Restore backend dependencies
echo "🔧 Restoring .NET backend dependencies..."
cd backend
dotnet restore
cd ..

# Install frontend dependencies
echo "🔧 Installing Angular frontend dependencies..."
cd frontend
npm install
cd ..

# Copy data contract models to frontend
echo "📋 Copying data contract models to frontend..."
cp datacontract/models.ts frontend/src/app/models/

echo "✅ Development environment setup complete!"
echo ""
echo "🎯 Next steps:"
echo "   1. Open two terminals"
echo "   2. In terminal 1: cd backend && dotnet run"
echo "   3. In terminal 2: cd frontend && npm start"
echo "   4. Open the forwarded ports to access the application"
echo ""
echo "📚 Documentation:"
echo "   - README.md - Project overview"
echo "   - docs/development.md - Development guide" 
echo "   - docs/codespaces.md - Codespaces guide"
echo ""
echo "Happy coding! 🎉"