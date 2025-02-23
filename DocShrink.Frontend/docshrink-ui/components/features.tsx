'use client'
import { FiZap, FiFileText, FiLock, FiDownload } from 'react-icons/fi';

export function Features() {
  const features = [
    {
      icon: <FiZap />, 
      title: 'Smart Compression',
      description: 'Reduce document size efficiently while maintaining original quality.'
    },
    {
      icon: <FiFileText />, 
      title: 'Multiple Format Support',
      description: 'Compress PDFs, DOCX, and XLSX files without altering their content.'
    },
    {
      icon: <FiLock />, 
      title: 'Fast and Secure',
      description: 'Quickly shrink files while keeping them safe and intact.'
    },
    {
      icon: <FiDownload />, 
      title: 'Easy Download & Sharing',
      description: 'Get compressed documents instantly, ready for use.'
    }
  ];

  return (
    <div className="bg-white rounded-lg p-6 shadow-sm">
      <h2 className="text-2xl font-semibold mb-6 text-gray-800">Features</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {features.map((feature, index) => (
          <div key={index} className="flex items-start space-x-4 p-4 border border-gray-200 rounded-lg hover:shadow-md transition-shadow duration-200">
            <div className="text-blue-500 text-2xl bg-blue-100 p-3 rounded-full">{feature.icon}</div>
            <div>
              <h3 className="text-lg font-semibold text-gray-700 mb-2">{feature.title}</h3>
              <p className="text-sm text-gray-600">{feature.description}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
