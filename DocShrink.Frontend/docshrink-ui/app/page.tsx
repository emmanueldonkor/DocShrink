
import { Uploader } from '@/components/uploader'
import { Features } from '@/components/features'

export default function Home() {
  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-4xl font-bold text-center mb-8 text-gray-800">DocShrink</h1>
      <div className="max-w-4xl mx-auto space-y-8">
        <Uploader />
        <Features />
      </div>
    </div>
  )
}