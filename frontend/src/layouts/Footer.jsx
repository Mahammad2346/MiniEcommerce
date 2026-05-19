import React from 'react';

function Footer() {
  return (
    <footer className="bg-slate-800 text-slate-400 text-center p-4 text-xs border-t border-slate-700 w-full mt-auto">
      &copy; {new Date().getFullYear()} MiniEcommerce. All rights reserved.
    </footer>
  );
}

export default Footer;
