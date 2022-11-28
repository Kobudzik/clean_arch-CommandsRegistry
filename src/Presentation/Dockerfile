	FROM node:16.18.1 as build-stage
	WORKDIR /app
	COPY package*.json ./
	RUN yarn install
	COPY . .
	RUN yarn build

	FROM nginx:1.21 as production-stage
	RUN rm /etc/nginx/conf.d/default.conf
	COPY nginx.conf /etc/nginx/conf.d
	COPY --from=build-stage /app/dist /usr/share/nginx/html
	EXPOSE 8080
	CMD ["nginx", "-g", "daemon off;"]