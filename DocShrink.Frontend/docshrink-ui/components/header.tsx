'use client'

import { FiZap } from 'react-icons/fi'
import { Button } from '@/components/ui/button'
import { useUser } from '@auth0/nextjs-auth0/client'

export function Header() {
  const { user} = useUser();

  return (
    <header className="bg-white shadow-sm sticky top-0 z-10">
      <div className="container mx-auto px-4 py-4 flex justify-between items-center">
        <div className="flex items-center space-x-2">
          <FiZap className="text-2xl text-blue-600" />
          <span className="font-bold text-2xl text-gray-800">DocShrink AI</span>
        </div>
        {!user ? (
          <a href="/api/auth/login">
            <Button className="text-blue-600 border-blue-600 hover:bg-blue-50">
              Login
            </Button>
          </a>
        ) : (
          <a href="/api/auth/logout">
            <Button className="text-red-600 border-red-600 hover:bg-red-50">
              Logout
            </Button>
          </a>
        )}
      </div>
    </header>
  )
}


