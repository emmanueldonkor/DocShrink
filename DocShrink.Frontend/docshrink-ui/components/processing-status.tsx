'use client'

import { useState, useEffect } from 'react'
import { Progress } from '@/components/ui/progress'
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card'

export function ProcessingStatus() {
  const [processing, setProcessing] = useState(false)
  const [progress, setProgress] = useState(0)

  useEffect(() => {
    // Simulate processing
    if (processing) {
      const interval = setInterval(() => {
        setProgress(prev => {
          if (prev >= 100) {
            clearInterval(interval)
            setProcessing(false)
            return 100
          }
          return prev + 10
        })
      }, 1000)
      return () => clearInterval(interval)
    }
  }, [processing])

  // Simulate starting the processing when component mounts
  useEffect(() => {
    setProcessing(true)
  }, [])

  if (!processing && progress === 0) return null

  return (
    <Card>
      <CardHeader>
        <CardTitle>Processing Status</CardTitle>
      </CardHeader>
      <CardContent>
        <Progress value={progress} className="w-full" />
        <p className="mt-2 text-center text-sm text-muted-foreground">
          {processing ? `Processing... ${progress}%` : 'Processing complete!'}
        </p>
      </CardContent>
    </Card>
  )
}
