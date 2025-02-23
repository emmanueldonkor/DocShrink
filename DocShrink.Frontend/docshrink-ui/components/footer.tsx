import { FiGithub, FiTwitter, FiLinkedin } from 'react-icons/fi'
export function Footer() {
  return (
    <footer className="bg-gray-900 text-white py-6 mt-12">
      <div className="container mx-auto flex flex-col md:flex-row justify-between items-center">
        <div className="text-sm">&copy; {new Date().getFullYear()} DocShrink by Emmanuel Eben Donkor. All rights reserved.</div>
        <div className="flex space-x-4 mt-4 md:mt-0">
          <a href="#" className="text-gray-400 hover:text-white transition-colors duration-300">
            <FiGithub size={22} />
          </a>
          <a href="#" className="text-gray-400 hover:text-white transition-colors duration-300">
            <FiTwitter size={22} />
          </a>
          <a href="#" className="text-gray-400 hover:text-white transition-colors duration-300">
            <FiLinkedin size={22} />
          </a>
        </div>
      </div>
    </footer>
  );
}
