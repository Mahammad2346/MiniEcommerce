import React, { useEffect } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import Header from './layouts/Header';
import Footer from './layouts/Footer';

function App() {
  const { isAuthenticated, getAccessTokenSilently, isLoading } = useAuth0();

  useEffect(() => {
    const getToken = async () => {
      if (isAuthenticated) {
        await getAccessTokenSilently();
      }
    };

    getToken();
  }, [isAuthenticated, getAccessTokenSilently]);

  if (isLoading) {
    return (
      <div className="min-h-screen bg-slate-900 text-slate-100 flex items-center justify-center font-medium text-base tracking-wide">
        Loading...
      </div>
    );
  }

  return (
    <div className="flex flex-col min-h-screen bg-white text-slate-800 antialiased">
      <Header />
      <main className="flex-grow container mx-auto px-6 py-10 max-w-6xl">
        
        {isAuthenticated ? (
          <div>
            <h1 className="text-3xl font-bold text-slate-900 tracking-tight mb-2">Products</h1>
            <h2 className="text-sm font-normal text-slate-400 tracking-wide uppercase">Catalog view</h2>
          </div>
        ) : (
          <div className="text-center py-20 max-w-md mx-auto">
            <h1 className="text-2xl font-bold text-slate-950 mb-3 tracking-tight">
              Welcome to MiniEcommerce
            </h1>
            <p className="text-slate-500 text-sm mb-6 leading-relaxed">
              Please sign in or register using the button in the top right corner to access our online catalog.
            </p>
          </div>
        )}
        
      </main>

      <Footer />
    </div>
  );
}

export default App;
