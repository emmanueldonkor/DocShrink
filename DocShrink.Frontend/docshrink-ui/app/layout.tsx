import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import { Toaster } from "@/components/ui/toaster";
import { Footer } from "@/components/footer";
import {UserProvider} from "@auth0/nextjs-auth0/client"


const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "DocShrink ",
  description: "Document Compression & Analysis",
};

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
    <UserProvider>
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      > 
        <main className="flex-grow">{children}</main>
        <Footer />
        <Toaster />
      </body>
      </UserProvider>
    </html>
  );
}


