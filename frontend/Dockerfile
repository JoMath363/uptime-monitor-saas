FROM node:20-alpine AS base

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

#DEVELOPMENT
FROM base AS Development
EXPOSE 4200
CMD ["npx", "ng", "serve", "--host", "0.0.0.0", "--poll=1000"]

#PRODUCTION
FROM base AS build
RUN npm run build
FROM nginx:alpine AS Production
COPY --from=build /app/dist/frontend/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]