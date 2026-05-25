import React from 'react';
import { useAuth0 } from '@auth0/auth0-react';

function Header() {
  const { loginWithRedirect, isAuthenticated, logout, user } = useAuth0();

  return (
    <nav className="bg-slate-800 text-white p-4 flex justify-between items-center shadow-md w-full">
      <h1 className="text-xl font-bold tracking-wider">MiniEcommerce</h1>
      <div className="space-x-4 flex items-center">
        <span className="cursor-pointer hover:text-slate-300">Products</span>
        {isAuthenticated ? (
          <div className="flex items-center gap-3">
            {user?.picture && (
              <img
                src={user.picture}
                alt={user.name}
                className="w-8 h-8 rounded-full border-2 border-blue-500"
              />
            )}
            <span className="text-sm font-medium hidden md:inline">{user?.name}</span>
            <button
              onClick={() => logout({ logoutParams: { returnTo: globalThis.location.origin } })}
              className="bg-red-600 hover:bg-red-700 px-3 py-1.5 rounded-md text-sm font-medium transition-colors"
            >
              Logout
            </button>
          </div>
        ) : (
          <button
            onClick={() => loginWithRedirect()}
            className="bg-blue-600 hover:bg-blue-700 px-4 py-1.5 rounded-md font-medium transition-colors"
          >
            Log in
          </button>
        )}
      </div>
    </nav>
  );
}

export default Header;
