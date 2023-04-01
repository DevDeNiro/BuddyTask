FROM node:lts-alpine

WORKDIR /app
COPY package*.json ./
RUN npm install
ENV PORT=5173
# Copie le reste de l'app
COPY . .
EXPOSE ${PORT}
CMD ["npm", "run", "dev"]
